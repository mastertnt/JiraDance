using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AtlassianCore;
using AtlassianCore.FieldManagement;
using AtlassianCore.Models;
using AtlassianCore.Models.Bitbucket;
using AtlassianCore.Models.Creation;
using AtlassianCore.Utility;
using Newtonsoft.Json;
using RestEase;

namespace JiraDance
{
    internal class Program
    {
        /// <summary>
        /// Current NLog.
        /// </summary>
        private static readonly NLog.Logger msLogger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Stores the technical settings.
        /// </summary>
        private static TechSettings settings;

        /// <summary>
        /// Creates a new feature.
        /// </summary>
        /// <param name="summary">The summary of the feature.</param>
        /// <param name="epicName">The epic names</param>
        /// <param name="description">The description of the feature.</param>
        /// <param name="acceptanceCritera">The acceptance criteria</param>
        /// <param name="priority">The priority.</param>
        /// <param name="projectKey">The project key.</param>
        /// <returns>The feature key.</returns>
        public static string CreateFeature(string summary, string epicName, string description, string acceptanceCritera, string priority, string projectKey)
        {
            return "";
        }

        /// <summary>
        /// Creates a new story.
        /// </summary>
        /// <param name="summary">The summary of the feature.</param>
        /// <param name="epicKey">The epic key</param>
        /// <param name="description">The description of the feature.</param>
        /// <param name="acceptanceCritera">The acceptance criteria</param>
        /// <param name="priority">The priority.</param>
        /// <param name="projectKey">The project key.</param>
        /// <returns>The feature key.</returns>
        public static string CreateUserStory(string summary, string epicKey, string description, string acceptanceCritera, string priority, string projectKey)
        {
            return "";
        }

        /// <summary>
        /// Creates a new issue.
        /// </summary>
        /// <param name="summary">The summary of the feature.</param>
        /// <param name="userStoryKey">The uset story key</param>
        /// <param name="description">The description of the feature.</param>
        /// <param name="issueType">The issue type</param>
        /// <param name="priority">The priority.</param>
        /// <param name="projectKey">The project key.</param>
        /// <returns>The feature key.</returns>
        public static string CreateSubIssue(string summary, string userStoryKey, string description, string issueType, string priority, string projectKey)
        {
            IJiraIssueApi api = null;
            if (string.IsNullOrWhiteSpace(settings.ResponseDebugPath) == false)
            {
                if (Directory.Exists(settings.ResponseDebugPath))
                {
                    api = new RestClient(settings.EndPoint) { ResponseDeserializer = new DebugResponseDeserializer() { DebugPath = settings.ResponseDebugPath }, RequestBodySerializer = new DebugRequestBodySerializer() { DebugPath = settings.ResponseDebugPath } }.For<IJiraIssueApi>();
                }
                else
                {
                    msLogger.Warn("Directory for debug does not exist " + settings.ResponseDebugPath);
                    api = RestClient.For<IJiraIssueApi>(settings.EndPoint);
                }
            }
            else
            {
                api = RestClient.For<IJiraIssueApi>(settings.EndPoint);
            }

            var value = Convert.ToBase64String(Encoding.ASCII.GetBytes(settings.Username + ":" + settings.Password));
            api.Authorization = new AuthenticationHeaderValue("Basic", value);


            IssueToCreate issueToCreate = new IssueToCreate();
            issueToCreate.Priority = priority;
            issueToCreate.ProjectKey = projectKey;
            issueToCreate.Summary = summary;
            issueToCreate.Type = issueType;
            issueToCreate.Description = description;

            JiraIssue createdIssue = api.CreateIssue(issueToCreate).Result;

            if (string.IsNullOrEmpty(userStoryKey) == false)
            {
                IssueLinkToCreate linkToCreate = new IssueLinkToCreate();
                linkToCreate.LinkType = "Relates";
                linkToCreate.InwardIssueKey = createdIssue.Key;
                linkToCreate.OutwardIssueKey = userStoryKey;
                api.LinkIssues(linkToCreate).Wait();
            }

            return createdIssue.Key;
        }

        public static void GetIssue(string key)
        {
            IJiraIssueApi api = null;
            if (string.IsNullOrWhiteSpace(settings.ResponseDebugPath) == false)
            {
                if (Directory.Exists(settings.ResponseDebugPath))
                {
                    api = new RestClient(settings.EndPoint) { ResponseDeserializer = new DebugResponseDeserializer() { DebugPath = settings.ResponseDebugPath }, RequestBodySerializer = new DebugRequestBodySerializer() { DebugPath = settings.ResponseDebugPath } }.For<IJiraIssueApi>();
                }
                else
                {
                    msLogger.Warn("Directory for debug does not exist " + settings.ResponseDebugPath);
                    api = RestClient.For<IJiraIssueApi>(settings.EndPoint);
                }
            }
            else
            {
                api = RestClient.For<IJiraIssueApi>(settings.EndPoint);
            }

            var value = Convert.ToBase64String(Encoding.ASCII.GetBytes(settings.Username + ":" + settings.Password));
            api.Authorization = new AuthenticationHeaderValue("Basic", value);

            api.GetIssue(key).Wait();
        }


        /// <summary>
        /// Main entry point of the application.
        /// </summary>
        /// <param name="args">The program arguments.</param>
        static void Main(string[] args)
        {
            try
            {
                //1tsky2ObocPatocN20Mp4F0C
                settings = TechSettings.Build(@".\techsettings.json");


                string key = CreateSubIssue("summary", "NBY-1", "description", "Bug", "Medium", "NBY");
                Console.WriteLine(key);
 

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
    }
}