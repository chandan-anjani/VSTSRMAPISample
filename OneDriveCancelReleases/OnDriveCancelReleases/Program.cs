using System;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Contracts;
using System.Diagnostics;

namespace OneDriveCancelReleases
{
    class Program
    {
        private static string url;
        private static string projectName;
        private static int definitionId;
        private static int numberOfDaysBefore;
        private static int numberOfDaysAfter;
        private static long totalDeploymentToCancel = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("========== Usage (Cancel all InProgress or NotDeployed deployments between two given number of days) ===================");
            Console.WriteLine("");
            Console.WriteLine("OneDriveCancelReleases.exe <ServerUrl> <ProjectName> <DefinitionId> <NumberOfDaysBefore> <NumberOfDaysAfter>");
            Console.WriteLine("e.g. OneDriveCancelReleases.exe https://anjani1.visualstudio.com \"My Project\" 62 30 60");
            Console.WriteLine("");
            Console.WriteLine("====================================");

            if (args.Length != 5)
            {
                Console.WriteLine("All arguments are not specified. Please refer the usage instructions");
            }

            url = args[0];
            projectName = args[1];

            if (!int.TryParse(args[2], out definitionId))
            {
                Console.WriteLine("Definition Id should be an integer");
            }

            if (!int.TryParse(args[3], out numberOfDaysBefore))
            {
                Console.WriteLine("NumberOfDaysToDelete Id should be an integer");
            }

            if (!int.TryParse(args[4], out numberOfDaysAfter))
            {
                Console.WriteLine("NumberOfDaysToDelete Id should be an integer");
            }

            Uri serverUrl = new Uri(url);
            VssCredentials credentials = new VssClientCredentials();
            credentials.Storage = new VssClientCredentialStorage();

            VssConnection connection = new VssConnection(serverUrl, credentials);

            ReleaseHttpClient2 rmClient = connection.GetClient<ReleaseHttpClient2>();
            CancelAllInProgressReleases(rmClient);
        }

        public static void CancelAllInProgressReleases(ReleaseHttpClient2 rmClient)
        {
            var maxModifiedTime = DateTime.Now.AddDays(-numberOfDaysBefore);
            var minModifiedTime = DateTime.Now.AddDays(-numberOfDaysAfter);

            try
            {
                int ctn = 0;
                IPagedCollection<Release> releases = null;
                do
                {
                    releases = rmClient.GetReleasesAsync2(
                    definitionId: definitionId,
                    statusFilter: ReleaseStatus.Active,
                    minCreatedTime: minModifiedTime,
                    maxCreatedTime: maxModifiedTime,
                    top: 100,
                    continuationToken: ctn,
                    expand: ReleaseExpands.Environments).Result;

                    ReleaseEnvironmentUpdateMetadata envMetaData = new ReleaseEnvironmentUpdateMetadata();
                    envMetaData.Status = EnvironmentStatus.Canceled;
                    envMetaData.Comment = "Good to cancel as it's pretty old";

                    foreach (var release in releases)
                    {
                        foreach (var environment in release.Environments)
                        {
                            if (environment.Status == EnvironmentStatus.Queued || environment.Status == EnvironmentStatus.InProgress)
                            {
                                try
                                {
                                    totalDeploymentToCancel++;
                                    Console.WriteLine("Cancelling the environment with releaseId: {0} ReleaseEnvironmentId: {1} ", release.Id, environment.Id);
                                    var result = rmClient.UpdateReleaseEnvironmentAsync(envMetaData, projectName, release.Id, environment.Id).Result;
                                    // rmClient.DeleteReleaseAsync(projectName, release.Id).SyncResult();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("=======================");
                                    Console.WriteLine("Not Cancelling the deployment with releaseId: {0} ReleaseEnvironmentId: {1} ", release.Id, environment.Id);
                                    Console.WriteLine(ex.Message);
                                    Console.WriteLine("=======================");
                                }
                            }
                        }
                    }

                    int.TryParse(releases.ContinuationToken, out ctn);
                } while (releases.ContinuationToken != null);

                Console.WriteLine("Total cancelled environments: {0}", totalDeploymentToCancel);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Could not get releases with given information");
            }
        }
    }
}
