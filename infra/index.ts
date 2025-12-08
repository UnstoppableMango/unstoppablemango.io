import * as crypto from 'node:crypto';
import * as fs from 'node:fs';
import * as path from 'node:path';
import * as pulumi from '@pulumi/pulumi';
import * as cloudflare from '@pulumi/cloudflare';
import * as command from '@pulumi/command';
import z from 'zod/v4';

const Cloudflare = z.object({
  accountId: z.string().length(32),
  zones: z.array(z.object({
    domainName: z.string().min(1),
  })),
});

type Cloudflare = z.infer<typeof Cloudflare>;

const config = new pulumi.Config();
const { accountId, zones } = Cloudflare.parse(config.requireObject('cloudflare'));

const assets = fs.readdirSync('../public', {
  recursive: true,
  encoding: 'utf8',
  withFileTypes: true,
});

const worker = new cloudflare.Worker('unstoppablemango', {
  accountId,
  name: 'unstoppablemango-io',
});

const domains: cloudflare.WorkersCustomDomain[] = [];

zones.forEach(({ domainName }) => {
  const zone = cloudflare.getZoneOutput({
    filter: { name: domainName },
  });

  if (!zone.zoneId) {
    throw new Error(`Zone ID not found for domain: ${domainName}`);
  }

  const zoneId = zone.zoneId.apply(z.string().min(1).parse);

  domains.push(new cloudflare.WorkersCustomDomain(domainName, {
    accountId,
    zoneId,
    environment: 'production',
    hostname: domainName,
    service: worker.name,
  }));

  new cloudflare.ZoneSetting(`${domainName}-ssl`, {
    zoneId,
    settingId: 'ssl',
    value: 'strict',
  });

  new cloudflare.ZoneSetting(`${domainName}-https`, {
    zoneId,
    settingId: 'automatic_https_rewrites',
    value: 'on',
  });
});

const root = path.resolve(__dirname, '..');

new command.local.Command('publish-worker', {
  create: pulumi.interpolate`npx wrangler deploy --name ${worker.name}`,
  dir: path.relative(__dirname, root),
  environment: {
    CLOUDFLARE_ACCOUNT_ID: accountId,
    CLOUDFLARE_API_TOKEN: new pulumi.Config('cloudflare').requireSecret('apiToken'),
  },
  triggers: assets.filter(x => !x.isDirectory())
    .map(({ name, parentPath }) => path.resolve(parentPath, name))
    .map(hashFile),
}, { dependsOn: [worker], additionalSecretOutputs: ['environment.CLOUDFLARE_API_TOKEN'] });

// export { originHostname };
// export const cdnURL = pulumi.interpolate`https://${cdnEndpoint.hostName}`;
// export const cdnHostname = cdnEndpoint.hostName;

function hashFile(file: string): string {
  const buf = fs.readFileSync(file);
  const hash = crypto.createHash('sha256');
  hash.update(buf);
  return hash.digest('hex');
}
