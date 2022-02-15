using System.Net;
using System.Net.Http.Headers;
using System.Text;
using AtlassianCore;
using AtlassianCore.FieldManagement;
using AtlassianCore.Models;
using AtlassianCore.Models.Bitbucket;
using AtlassianCore.Utility;
using RestEase;

namespace JiraDance
{
    internal class Program
    {
        /// <summary>
        /// Current NLog.
        /// </summary>
        private static readonly NLog.Logger msLogger = NLog.LogManager.GetCurrentClassLogger();

        private static TechSettings settings;


        /// <summary>
        /// Main entry point of the application.
        /// </summary>
        /// <param name="args">The program arguments.</param>
        static void Main(string[] args)
        {
            try
            {
                settings = TechSettings.Build(@".\techsettings.json");
                
                IBitbucketApi api = null;
                if (string.IsNullOrWhiteSpace(settings.ResponseDebugPath) == false)
                {
                    if (Directory.Exists(settings.ResponseDebugPath))
                    {
                        api = RestClient.For<IBitbucketApi>(settings.EndPoint, new DebugResponseDeserializer() { DebugPath = settings.ResponseDebugPath }, new DebugRequestBodySerializer() { DebugPath = settings.ResponseDebugPath });
                    }
                    else
                    {
                        msLogger.Warn("Directory for debug does not exist " + settings.ResponseDebugPath);
                        api = RestClient.For<IBitbucketApi>(settings.EndPoint);
                    }
                }
                else
                {
                    api = RestClient.For<IBitbucketApi>(settings.EndPoint);
                }

                var value = Convert.ToBase64String(Encoding.ASCII.GetBytes(settings.Username + ":" + settings.Password));
                api.Authorization = new AuthenticationHeaderValue("Basic", settings.Password);

                Task<List<PullRequest>> pullRequests = api.GetAllPullRequests("nby1" , "nby");
                foreach (var pullRequest in pullRequests.Result)

                {
                    Console.WriteLine(pullRequest);
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
    }
}