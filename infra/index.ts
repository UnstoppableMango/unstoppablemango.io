import * as pulumi from '@pulumi/pulumi';
import * as azure from '@pulumi/azure-native';

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

export const url = pulumi.interpolate`https://${site.defaultHostname}`;
