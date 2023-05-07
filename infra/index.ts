import * as pulumi from '@pulumi/pulumi';
import * as azure from '@pulumi/azure-native';
import * as cloudflare from '@pulumi/cloudflare';

const config = new pulumi.Config();
const repositoryToken = config.requireSecret('repositoryToken');

const resourceGroupName = 'UnstoppableMango.io';
const resourceGroup = new azure.resources.ResourceGroup(resourceGroupName, {
  resourceGroupName,
});
const domainName = 'unstoppablemango.io';
const mangioDomainName = 'unstoppablemang.io';
const unstoppableMangoZoneId = 'de10a9e5057761cf8b2151d80dd684fa';
const unstoppableMangZoneId = '24f3d181ad1b22a5e290175096171516';

const site = new azure.web.StaticSite('app', {
  resourceGroupName: resourceGroup.name,
  sku: {
    tier: 'Free',
    name: 'Free',
  },
  repositoryUrl: 'https://github.com/UnstoppableMango/unstoppablemango.io',
  repositoryToken,
  branch: 'main',
  buildProperties: {
    appLocation: '/public',
    skipGithubActionWorkflowGeneration: true,
    githubActionSecretNameOverride: 'AZURE_STATIC_WEB_APPS_API_TOKEN',
  },
});

const customDomain = new azure.web.StaticSiteCustomDomain(
  'unstoppablemango.io',
  {
    name: site.name,
    resourceGroupName: resourceGroup.name,
    domainName: 'unstoppablemango.io',
    validationMethod: 'dns-txt-token',
  },
  {
    protect: true,
  }
);

const primaryCnameRecord = new cloudflare.Record('unstoppablemango.io-cname', {
  name: domainName,
  zoneId: unstoppableMangoZoneId,
  value: site.defaultHostname,
  type: 'CNAME',
  proxied: true,
});

const unstoppablemangoSettings = new cloudflare.ZoneSettingsOverride(
  'unstoppablemango.io',
  {
    zoneId: unstoppableMangoZoneId,
    settings: {
      ssl: 'strict',
      alwaysUseHttps: 'on',
    },
  }
);

const mangioCnameRecord = new cloudflare.Record('unstoppablemang.io-cname', {
  name: domainName,
  zoneId: unstoppableMangZoneId,
  value: site.defaultHostname,
  type: 'CNAME',
  proxied: true,
});

const test = new cloudflare.Ruleset('mangio-redirect', {
  zoneId: unstoppableMangZoneId,
  name: 'Redirect mangio to mangoio',
  kind: 'zone',
  phase: 'http_request_dynamic_redirect',
  rules: [
    {
      expression: '',
      action: 'redirect',
    },
  ],
});

const test2 = new cloudflare.List('mangio-redirect', {
  name: 'mangio_redirect',
  accountId: '265a046434c952eeecb9710cfd76617c',
  kind: 'redirct',
  items: [
    {
    value: {
      redirects: [
        {
          sourceUrl: mangioDomainName,
          targetUrl: `https://${domainName}`,
        }
      ]
    }
    }
  ]
});

export const validationToken = customDomain.validationToken;
export const url = pulumi.interpolate`https://${site.defaultHostname}`;
