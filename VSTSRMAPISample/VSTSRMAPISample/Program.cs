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


        static void Main(string[] args)
        {
            Uri serverUrl = new Uri(vstsUrl);
            VssCredentials credentials = new VssClientCredentials();
            credentials.Storage = new VssClientCredentialStorage();

            VssConnection connection = new VssConnection(serverUrl, credentials);

            ReleaseHttpClient rmClient = connection.GetClient<ReleaseHttpClient>();
        }


    }
}
