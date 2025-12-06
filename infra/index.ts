import * as fs from 'node:fs';
import * as path from 'node:path';
import * as pulumi from '@pulumi/pulumi';
import * as azure from '@pulumi/azure-native';
import * as cloudflare from '@pulumi/cloudflare';

const domainName = 'unstoppablemango.io';
// const mangioDomainName = 'unstoppablemang.io';

const resourceGroupName = 'UnstoppableMango.io';
const resourceGroup = new azure.resources.ResourceGroup(resourceGroupName, {
  resourceGroupName,
});

const storageAccount = new azure.storage.StorageAccount('website', {
  resourceGroupName: resourceGroup.name,
  kind: azure.storage.Kind.StorageV2,
  enableHttpsTrafficOnly: true,
  allowBlobPublicAccess: true,
  publicNetworkAccess: azure.storage.PublicNetworkAccess.Enabled,
  minimumTlsVersion: azure.storage.MinimumTlsVersion.TLS1_2,
  // customDomain: {
  //   name: domainName,
  // },
  sku: {
    name: azure.storage.SkuName.Standard_LRS,
  },
});

const staticWebsite = new azure.storage.StorageAccountStaticWebsite('website', {
  resourceGroupName: resourceGroup.name,
  accountName: storageAccount.name,
  indexDocument: 'index.html',
  error404Document: 'error.html',
});

fs.readdirSync('../public', {
  recursive: true,
  encoding: 'utf8',
  withFileTypes: true,
}).forEach((dirent) => {
  if (!dirent.isFile()) return;

  const assetPath = path.resolve(dirent.parentPath, dirent.name);
  const relativePath = path.relative('../public', assetPath);
  const ext = path.extname(dirent.name);

  new azure.storage.Blob(dirent.name, {
    resourceGroupName: resourceGroup.name,
    accountName: storageAccount.name,
    containerName: staticWebsite.containerName,
    blobName: relativePath,
    type: azure.storage.BlobType.Block,
    accessTier: azure.storage.AccessTier.Cool,
    source: new pulumi.asset.FileAsset(assetPath),
    contentType: ((): string => {
      switch (ext) {
        case '.html':
          return 'text/html';
        case '.css':
          return 'text/css';
        case '.js':
          return 'application/javascript';
        default:
          return 'application/octet-stream';
      }
    })(),
  });
});

export const originURL = storageAccount.primaryEndpoints.web;
const originHostname = originURL.apply(x => new URL(x).hostname);

// const cdnEndpoint = new azure.cdn.Endpoint('cdn-endpoint', {
//   resourceGroupName: resourceGroup.name,
//   endpointName: 'unstoppablemango-io-endpoint',
//   profileName: cdn.name,
//   isHttpAllowed: false,
//   isHttpsAllowed: true,
//   isCompressionEnabled: true,
//   originHostHeader: originHostname,
//   origins: [{
//     name: storageAccount.name.apply((name) => `${name}-origin`),
//     hostName: originURL.apply(
//       (url) => url.replace('https://', '').replace('/', ''),
//     ),
//   }],
//   contentTypesToCompress: [
//     'text/html',
//     'text/css',
//     'application/javascript',
//     'image/png',
//     'image/jpeg',
//   ],
// });

const unstoppableMangoZoneId = 'de10a9e5057761cf8b2151d80dd684fa';
// const unstoppableMangZoneId = '24f3d181ad1b22a5e290175096171516';

new azure.dns.RecordSet('test', {
  resourceGroupName: resourceGroup.name,
  zoneName: domainName,
  recordType: 'CNAME',
  cnameRecord: {
    cname: originHostname,
  },
});

new cloudflare.DnsRecord('unstoppablemango.io-cname', {
  name: domainName,
  zoneId: unstoppableMangoZoneId,
  content: originHostname,
  type: 'CNAME',
  proxied: true,
  ttl: 1, // Automatic
});

new cloudflare.ZoneSetting('unstoppablemango.io-ssl', {
  zoneId: unstoppableMangoZoneId,
  settingId: 'ssl',
  value: 'strict',
});

new cloudflare.ZoneSetting('unstoppablemango.io-https', {
  zoneId: unstoppableMangoZoneId,
  settingId: 'automatic_https_rewrites',
  value: 'on',
});

export { originHostname };
// export const cdnURL = pulumi.interpolate`https://${cdnEndpoint.hostName}`;
// export const cdnHostname = cdnEndpoint.hostName;
