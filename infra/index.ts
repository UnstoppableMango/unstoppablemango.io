import * as pulumi from '@pulumi/pulumi';
import * as azure from '@pulumi/azure-native';

const config = new pulumi.Config();
const repositoryToken = config.requireSecret('repositoryToken');

const resourceGroupName = 'UnstoppableMango.io';
const resourceGroup = new azure.resources.ResourceGroup(resourceGroupName, {
  resourceGroupName,
});

const site = new azure.web.StaticSite('app', {
  name: 'app',
  resourceGroupName: resourceGroup.name,
  sku: {
    tier: 'Free',
    name: 'Free',
  },
  repositoryUrl: 'https://github.com/UnstoppableMango/unstoppablemango.io',
  repositoryToken,
  branch: 'main',
  buildProperties: {
    appBuildCommand: 'npm run build',
    appLocation: '/public',
    outputLocation: '/public',
  },
});

export const url = pulumi.interpolate`https://${site.defaultHostname}`;
