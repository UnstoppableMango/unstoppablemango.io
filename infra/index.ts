import * as fs from 'node:fs';
import * as path from 'node:path';
import * as pulumi from '@pulumi/pulumi';
import * as azure from '@pulumi/azure-native';
import * as cloudflare from '@pulumi/cloudflare';

const resourceGroupName = 'UnstoppableMango.io';
const resourceGroup = new azure.resources.ResourceGroup(resourceGroupName, {
  resourceGroupName,
});

const cdn = new azure.cdn.Profile('unstoppablemango-io', {
  resourceGroupName: resourceGroup.name,
  sku: {
    name: azure.cdn.SkuName.Standard_Microsoft,
  },
});

const storageAccount = new azure.storage.StorageAccount('website', {
  resourceGroupName: resourceGroup.name,
  kind: azure.storage.Kind.StorageV2,
  enableHttpsTrafficOnly: true,
  sku: {
    name: azure.storage.SkuName.Standard_LRS,
  },
});

const staticWebsite = new azure.storage.StorageAccountStaticWebsite('website', {
  accountName: storageAccount.name,
  resourceGroupName: resourceGroup.name,
  indexDocument: 'index.html',
  error404Document: 'error.html',
});

fs.readdirSync('../public', {
  recursive: true,
  encoding: 'utf8',
  withFileTypes: true,
}).forEach((dirent) => {
  if (!dirent.isFile()) return;

  new azure.storage.Blob(dirent.name, {
    resourceGroupName: resourceGroup.name,
    accountName: storageAccount.name,
    containerName: staticWebsite.containerName,
    source: new pulumi.asset.FileAsset(path.resolve(dirent.parentPath, dirent.name)),
    contentType: ((): string => {
      switch (path.extname(dirent.name)) {
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

const cdnEndpoint = new azure.cdn.Endpoint('cdn-endpoint', {
  resourceGroupName: resourceGroup.name,
  endpointName: 'unstoppablemango-io-endpoint',
  profileName: cdn.name,
  isHttpAllowed: false,
  isHttpsAllowed: true,
  isCompressionEnabled: true,
  originHostHeader: originHostname,
  origins: [{
    name: storageAccount.name.apply((name) => `${name}-origin`),
    hostName: originURL.apply(
      (url) => url.replace('https://', '').replace('/', ''),
    ),
  }],
  contentTypesToCompress: [
    'text/html',
    'text/css',
    'application/javascript',
    'image/png',
    'image/jpeg',
  ],
});

const domainName = 'unstoppablemango.io';
// const mangioDomainName = 'unstoppablemang.io';
const unstoppableMangoZoneId = 'de10a9e5057761cf8b2151d80dd684fa';
// const unstoppableMangZoneId = '24f3d181ad1b22a5e290175096171516';

new cloudflare.DnsRecord('unstoppablemango.io-cname', {
  name: domainName,
  zoneId: unstoppableMangoZoneId,
  content: cdnEndpoint.hostName,
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
export const cdnURL = pulumi.interpolate`https://${cdnEndpoint.hostName}`;
export const cdnHostname = cdnEndpoint.hostName;
