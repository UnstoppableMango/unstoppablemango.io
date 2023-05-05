import * as pulumi from '@pulumi/pulumi';
import * as azure from '@pulumi/azure-native';
import * as cloudflare from '@pulumi/cloudflare';

const config = new pulumi.Config();
const repositoryToken = config.requireSecret('repositoryToken');

const resourceGroupName = 'UnstoppableMango.io';
const resourceGroup = new azure.resources.ResourceGroup(resourceGroupName, {
  resourceGroupName,
});

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
    validationMethod: 'dns-txt-token'
  }
);

const primaryTxtRecord = new cloudflare.Record('unstoppablemango.io', {
  name: customDomain.domainName,
  zoneId: 'de10a9e5057761cf8b2151d80dd684fa',
  value: customDomain.validationToken,
  type: 'TXT',
  comment: 'Validation record for Azure',
});

export const url = pulumi.interpolate`https://${site.defaultHostname}`;
