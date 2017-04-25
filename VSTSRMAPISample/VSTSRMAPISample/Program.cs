﻿using System;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;
using Microsoft.VisualStudio.Services.WebApi;
using Newtonsoft.Json;

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

           /* Uncomment the scenario as per your need
            * Scenario1 :- Deploy a particular environment 
            var releaseDefinition = GetReleaseDefintion(rmClient, releaseDefinitionId);

            var release = CreateRelease(rmClient, releaseDefinition);
            const int environmentIdToDeploy = 3;
            var releaseEnvironment = DeployAnEnvironment(rmClient, release.Id, release.Environments[environmentIdToDeploy].Id);
            */

            /* Scenario2: Create release defintion using given json */

            var rdJson = "{\"source\":2,\"id\":1,\"revision\":2,\"name\":\"SampleReleaseDefinition\",\"createdBy\":{\"id\":\"8af33462-3b72-4a0f-bde1-815b1c12e64b\",\"displayName\":\"Anjani\",\"uniqueName\":\"chandan.anjani@outlook.com\",\"url\":\"https://app.vssps.visualstudio.com/A14019f76-dfe4-49c8-a750-40f5b70b1595/_apis/Identities/8af33462-3b72-4a0f-bde1-815b1c12e64b\",\"imageUrl\":\"https://anjani1.visualstudio.com/_api/_common/identityImage?id=8af33462-3b72-4a0f-bde1-815b1c12e64b\"},\"createdOn\":\"2017-04-04T08:45:34.320Z\",\"modifiedBy\":{\"id\":\"8af33462-3b72-4a0f-bde1-815b1c12e64b\",\"displayName\":\"Anjani\",\"uniqueName\":\"chandan.anjani@outlook.com\",\"url\":\"https://app.vssps.visualstudio.com/A14019f76-dfe4-49c8-a750-40f5b70b1595/_apis/Identities/8af33462-3b72-4a0f-bde1-815b1c12e64b\",\"imageUrl\":\"https://anjani1.visualstudio.com/_api/_common/identityImage?id=8af33462-3b72-4a0f-bde1-815b1c12e64b\"},\"modifiedOn\":\"2017-04-04T08:53:25.893Z\",\"lastRelease\":null,\"variables\":{},\"variableGroups\":[],\"environments\":[{\"id\":1,\"name\":\"QA\",\"rank\":1,\"owner\":{\"id\":\"8af33462-3b72-4a0f-bde1-815b1c12e64b\",\"displayName\":\"Anjani\",\"uniqueName\":\"chandan.anjani@outlook.com\",\"url\":\"https://app.vssps.visualstudio.com/A14019f76-dfe4-49c8-a750-40f5b70b1595/_apis/Identities/8af33462-3b72-4a0f-bde1-815b1c12e64b\",\"imageUrl\":\"https://anjani1.visualstudio.com/_api/_common/identityImage?id=8af33462-3b72-4a0f-bde1-815b1c12e64b\"},\"variables\":{},\"preDeployApprovals\":{\"approvals\":[{\"rank\":1,\"isAutomated\":true,\"isNotificationOn\":false,\"id\":1}]},\"deployStep\":{\"tasks\":[],\"id\":8},\"postDeployApprovals\":{\"approvals\":[{\"rank\":1,\"isAutomated\":true,\"isNotificationOn\":false,\"id\":9}]},\"deployPhases\":[{\"deploymentInput\":{\"parallelExecution\":{\"parallelExecutionType\":\"none\"},\"skipArtifactsDownload\":false,\"timeoutInMinutes\":0,\"queueId\":14,\"demands\":[],\"enableAccessToken\":false},\"rank\":1,\"phaseType\":1,\"name\":\"Run on agent\",\"workflowTasks\":[]}],\"environmentOptions\":{\"emailNotificationType\":\"OnlyOnFailure\",\"emailRecipients\":\"release.environment.owner;release.creator\",\"skipArtifactsDownload\":false,\"timeoutInMinutes\":0,\"enableAccessToken\":false},\"demands\":[],\"conditions\":[{\"name\":\"ReleaseStarted\",\"conditionType\":1,\"value\":\"\"}],\"executionPolicy\":{\"concurrencyCount\":0,\"queueDepthCount\":0},\"schedules\":[],\"retentionPolicy\":{\"daysToKeep\":30,\"releasesToKeep\":3,\"retainBuild\":true}},{\"id\":2,\"name\":\"Dev\",\"rank\":2,\"owner\":{\"id\":\"8af33462-3b72-4a0f-bde1-815b1c12e64b\",\"displayName\":\"Anjani\",\"uniqueName\":\"chandan.anjani@outlook.com\",\"url\":\"https://app.vssps.visualstudio.com/A14019f76-dfe4-49c8-a750-40f5b70b1595/_apis/Identities/8af33462-3b72-4a0f-bde1-815b1c12e64b\",\"imageUrl\":\"https://anjani1.visualstudio.com/_api/_common/identityImage?id=8af33462-3b72-4a0f-bde1-815b1c12e64b\"},\"variables\":{},\"preDeployApprovals\":{\"approvals\":[{\"rank\":1,\"isAutomated\":true,\"isNotificationOn\":false,\"id\":2}]},\"deployStep\":{\"tasks\":[],\"id\":7},\"postDeployApprovals\":{\"approvals\":[{\"rank\":1,\"isAutomated\":true,\"isNotificationOn\":false,\"id\":10}]},\"deployPhases\":[{\"deploymentInput\":{\"parallelExecution\":{\"parallelExecutionType\":\"none\"},\"skipArtifactsDownload\":false,\"timeoutInMinutes\":0,\"queueId\":14,\"demands\":[],\"enableAccessToken\":false},\"rank\":1,\"phaseType\":1,\"name\":\"Run on agent\",\"workflowTasks\":[]}],\"environmentOptions\":{\"emailNotificationType\":\"OnlyOnFailure\",\"emailRecipients\":\"release.environment.owner;release.creator\",\"skipArtifactsDownload\":false,\"timeoutInMinutes\":0,\"enableAccessToken\":false},\"demands\":[],\"conditions\":[{\"name\":\"QA\",\"conditionType\":2,\"value\":\"4\"}],\"executionPolicy\":{\"concurrencyCount\":0,\"queueDepthCount\":0},\"schedules\":[],\"retentionPolicy\":{\"daysToKeep\":30,\"releasesToKeep\":3,\"retainBuild\":true}},{\"id\":3,\"name\":\"Pre-PROD\",\"rank\":3,\"owner\":{\"id\":\"8af33462-3b72-4a0f-bde1-815b1c12e64b\",\"displayName\":\"Anjani\",\"uniqueName\":\"chandan.anjani@outlook.com\",\"url\":\"https://app.vssps.visualstudio.com/A14019f76-dfe4-49c8-a750-40f5b70b1595/_apis/Identities/8af33462-3b72-4a0f-bde1-815b1c12e64b\",\"imageUrl\":\"https://anjani1.visualstudio.com/_api/_common/identityImage?id=8af33462-3b72-4a0f-bde1-815b1c12e64b\"},\"variables\":{},\"preDeployApprovals\":{\"approvals\":[{\"rank\":1,\"isAutomated\":true,\"isNotificationOn\":false,\"id\":3}]},\"deployStep\":{\"tasks\":[],\"id\":6},\"postDeployApprovals\":{\"approvals\":[{\"rank\":1,\"isAutomated\":true,\"isNotificationOn\":false,\"id\":11}]},\"deployPhases\":[{\"deploymentInput\":{\"parallelExecution\":{\"parallelExecutionType\":\"none\"},\"skipArtifactsDownload\":false,\"timeoutInMinutes\":0,\"queueId\":14,\"demands\":[],\"enableAccessToken\":false},\"rank\":1,\"phaseType\":1,\"name\":\"Run on agent\",\"workflowTasks\":[]}],\"environmentOptions\":{\"emailNotificationType\":\"OnlyOnFailure\",\"emailRecipients\":\"release.environment.owner;release.creator\",\"skipArtifactsDownload\":false,\"timeoutInMinutes\":0,\"enableAccessToken\":false},\"demands\":[],\"conditions\":[{\"name\":\"Dev\",\"conditionType\":2,\"value\":\"4\"}],\"executionPolicy\":{\"concurrencyCount\":0,\"queueDepthCount\":0},\"schedules\":[],\"retentionPolicy\":{\"daysToKeep\":30,\"releasesToKeep\":3,\"retainBuild\":true}},{\"id\":4,\"name\":\"PROD\",\"rank\":4,\"owner\":{\"id\":\"8af33462-3b72-4a0f-bde1-815b1c12e64b\",\"displayName\":\"Anjani\",\"uniqueName\":\"chandan.anjani@outlook.com\",\"url\":\"https://app.vssps.visualstudio.com/A14019f76-dfe4-49c8-a750-40f5b70b1595/_apis/Identities/8af33462-3b72-4a0f-bde1-815b1c12e64b\",\"imageUrl\":\"https://anjani1.visualstudio.com/_api/_common/identityImage?id=8af33462-3b72-4a0f-bde1-815b1c12e64b\"},\"variables\":{},\"preDeployApprovals\":{\"approvals\":[{\"rank\":1,\"isAutomated\":true,\"isNotificationOn\":false,\"id\":4}]},\"deployStep\":{\"tasks\":[],\"id\":5},\"postDeployApprovals\":{\"approvals\":[{\"rank\":1,\"isAutomated\":true,\"isNotificationOn\":false,\"id\":12}]},\"deployPhases\":[{\"deploymentInput\":{\"parallelExecution\":{\"parallelExecutionType\":\"none\"},\"skipArtifactsDownload\":false,\"timeoutInMinutes\":0,\"queueId\":14,\"demands\":[],\"enableAccessToken\":false},\"rank\":1,\"phaseType\":1,\"name\":\"Run on agent\",\"workflowTasks\":[]}],\"environmentOptions\":{\"emailNotificationType\":\"OnlyOnFailure\",\"emailRecipients\":\"release.environment.owner;release.creator\",\"skipArtifactsDownload\":false,\"timeoutInMinutes\":0,\"enableAccessToken\":false},\"demands\":[],\"conditions\":[],\"executionPolicy\":{\"concurrencyCount\":0,\"queueDepthCount\":0},\"schedules\":[],\"retentionPolicy\":{\"daysToKeep\":30,\"releasesToKeep\":3,\"retainBuild\":true}}],\"artifacts\":[],\"triggers\":[],\"releaseNameFormat\":\"Release-$(rev:r)\",\"url\":\"https://anjani1.vsrm.visualstudio.com/99c5f49e-b74a-45c3-bf95-3c753992adc6/_apis/Release/definitions/1\",\"_links\":{\"self\":{\"href\":\"https://anjani1.vsrm.visualstudio.com/99c5f49e-b74a-45c3-bf95-3c753992adc6/_apis/Release/definitions/1\"},\"web\":{\"href\":\"https://anjani1.visualstudio.com/99c5f49e-b74a-45c3-bf95-3c753992adc6/_apps/hub/ms.vss-releaseManagement-web.hub-explorer?definitionId=1\"}},\"tags\":[],\"properties\":{}}";

            var newDefinition = CreateReleaseDefinition(rmClient, rdJson);
        }

        static ReleaseDefinition CreateReleaseDefinition(ReleaseHttpClient rmClient, string rdJson)
        {
            var definition = JsonConvert.DeserializeObject<ReleaseDefinition>(rdJson);
            definition.CreatedBy = null;
            definition.ModifiedBy = null;
            definition.Name = string.Format("{0} - Copy ", definition.Name);

            var newDefinition = rmClient.CreateReleaseDefinitionAsync(definition, projectName).Result;
            return newDefinition;
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
