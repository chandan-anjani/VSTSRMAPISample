using System;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;
using Microsoft.VisualStudio.Services.WebApi;

namespace VSTSRMAPISample
{
    class Program
    {
        public static string vstsUrl = "https://anjani1.visualstudio.com";
        public static string projectName = "RMAPISample";
        public static int releaseDefinitionId = 1;

        static void Main(string[] args)
        {
            Uri serverUrl = new Uri(vstsUrl);
            VssCredentials credentials = new VssClientCredentials();
            credentials.Storage = new VssClientCredentialStorage();

            VssConnection connection = new VssConnection(serverUrl, credentials);

            ReleaseHttpClient rmClient = connection.GetClient<ReleaseHttpClient>();

            var releaseDefinition = GetReleaseDefintion(rmClient, releaseDefinitionId);

            var release = CreateRelease(rmClient, releaseDefinition);

            var releaseEnvironment = DeployAnEnvironment(rmClient, release.Id, release.Environments[3].Id);
        }

       static ReleaseDefinition GetReleaseDefintion(ReleaseHttpClient rmClient, int releaseDefintionId)
        {
          return rmClient.GetReleaseDefinitionAsync(projectName, releaseDefintionId).Result;
        }

        static Release CreateRelease(ReleaseHttpClient rmClient, ReleaseDefinition releaseDefinition)
        {
            ReleaseStartMetadata rmMetaData = new ReleaseStartMetadata();
            rmMetaData.DefinitionId = releaseDefinition.Id;

            return rmClient.CreateReleaseAsync(rmMetaData, project: projectName).Result;
        }

        static ReleaseEnvironment DeployAnEnvironment(ReleaseHttpClient rmClient, int releaseId, int environmentIdToDeploy)
        {
            ReleaseEnvironmentUpdateMetadata envMetaData = new ReleaseEnvironmentUpdateMetadata();
            envMetaData.Status = EnvironmentStatus.InProgress;
            envMetaData.Comment = "Good to go";
            return rmClient.UpdateReleaseEnvironmentAsync(envMetaData, projectName, releaseId, environmentIdToDeploy).Result;
        }
    }
}
