using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using CsvHelper;
using CsvHelper.Excel;
using JiraTemplateExecute;
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
        private static TechSettings msSettings;

        /// <summary>
        /// Stores the custom fields.
        /// </summary>
        private static List<JiraCustomField> msCustomFields;

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
        public static string CreateEpic(string summary, string epicName, string description, string acceptanceCritera, string priority, string projectKey, string blockedBy)
        {
            IJiraIssueApi api = null;
            if (string.IsNullOrWhiteSpace(msSettings.ResponseDebugPath) == false)
            {
                if (Directory.Exists(msSettings.ResponseDebugPath))
                {
                    api = new RestClient(msSettings.EndPoint) { ResponseDeserializer = new DebugResponseDeserializer() { DebugPath = msSettings.ResponseDebugPath }, RequestBodySerializer = new DebugRequestBodySerializer() { DebugPath = msSettings.ResponseDebugPath } }.For<IJiraIssueApi>();
                }
                else
                {
                    msLogger.Warn("Directory for debug does not exist " + msSettings.ResponseDebugPath);
                    api = RestClient.For<IJiraIssueApi>(msSettings.EndPoint);
                }
            }
            else
            {
                api = RestClient.For<IJiraIssueApi>(msSettings.EndPoint);
            }

            var value = Convert.ToBase64String(Encoding.ASCII.GetBytes(msSettings.Username + ":" + msSettings.Password));
            api.Authorization = new AuthenticationHeaderValue("Basic", value);


            IssueToCreate issueToCreate = new IssueToCreate();
            issueToCreate.Priority = priority;
            issueToCreate.ProjectKey = projectKey;
            issueToCreate.Summary = summary;
            issueToCreate.Type = "Epic";
            issueToCreate.Description = description;

            // Add Epic name.
            issueToCreate.CustomFields.Add("fields." + msCustomFields.FirstOrDefault(cf => cf.Name == "Epic Name").Id, epicName);

            JiraIssue createdIssue = api.CreateIssue(issueToCreate).Result;

            if (string.IsNullOrEmpty(blockedBy) == false)
            {
                IssueLinkToCreate linkToCreate = new IssueLinkToCreate();
                linkToCreate.LinkType = "Blocks";
                linkToCreate.InwardIssueKey = createdIssue.Key;
                linkToCreate.OutwardIssueKey = blockedBy;
                api.LinkIssues(linkToCreate).Wait();
            }

            return createdIssue.Key;
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
        public static string CreateTask(string summary, string userStoryKey, string description, string issueType, string priority, string projectKey)
        {
            IJiraIssueApi api = null;
            if (string.IsNullOrWhiteSpace(msSettings.ResponseDebugPath) == false)
            {
                if (Directory.Exists(msSettings.ResponseDebugPath))
                {
                    api = new RestClient(msSettings.EndPoint) { ResponseDeserializer = new DebugResponseDeserializer() { DebugPath = msSettings.ResponseDebugPath }, RequestBodySerializer = new DebugRequestBodySerializer() { DebugPath = msSettings.ResponseDebugPath } }.For<IJiraIssueApi>();
                }
                else
                {
                    msLogger.Warn("Directory for debug does not exist " + msSettings.ResponseDebugPath);
                    api = RestClient.For<IJiraIssueApi>(msSettings.EndPoint);
                }
            }
            else
            {
                api = RestClient.For<IJiraIssueApi>(msSettings.EndPoint);
            }

            var value = Convert.ToBase64String(Encoding.ASCII.GetBytes(msSettings.Username + ":" + msSettings.Password));
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
                linkToCreate.LinkType = "Blocks";
                linkToCreate.InwardIssueKey = createdIssue.Key;
                linkToCreate.OutwardIssueKey = userStoryKey;
                api.LinkIssues(linkToCreate).Wait();
            }

            return createdIssue.Key;
        }

        public static void GetIssue(string key)
        {
            IJiraIssueApi api = null;
            if (string.IsNullOrWhiteSpace(msSettings.ResponseDebugPath) == false)
            {
                if (Directory.Exists(msSettings.ResponseDebugPath))
                {
                    api = new RestClient(msSettings.EndPoint) { ResponseDeserializer = new DebugResponseDeserializer() { DebugPath = msSettings.ResponseDebugPath }, RequestBodySerializer = new DebugRequestBodySerializer() { DebugPath = msSettings.ResponseDebugPath } }.For<IJiraIssueApi>();
                }
                else
                {
                    msLogger.Warn("Directory for debug does not exist " + msSettings.ResponseDebugPath);
                    api = RestClient.For<IJiraIssueApi>(msSettings.EndPoint);
                }
            }
            else
            {
                api = RestClient.For<IJiraIssueApi>(msSettings.EndPoint);
            }

            var value = Convert.ToBase64String(Encoding.ASCII.GetBytes(msSettings.Username + ":" + msSettings.Password));
            api.Authorization = new AuthenticationHeaderValue("Basic", value);

            api.GetIssue(key).Wait();
        }

        public static IJiraIssueApi GetApi()
        {
            IJiraIssueApi api = null;
            if (string.IsNullOrWhiteSpace(msSettings.ResponseDebugPath) == false)
            {
                if (Directory.Exists(msSettings.ResponseDebugPath))
                {
                    api = new RestClient(msSettings.EndPoint) { ResponseDeserializer = new DebugResponseDeserializer() { DebugPath = msSettings.ResponseDebugPath }, RequestBodySerializer = new DebugRequestBodySerializer() { DebugPath = msSettings.ResponseDebugPath } }.For<IJiraIssueApi>();
                }
                else
                {
                    msLogger.Warn("Directory for debug does not exist " + msSettings.ResponseDebugPath);
                    api = RestClient.For<IJiraIssueApi>(msSettings.EndPoint);
                }
            }
            else
            {
                api = RestClient.For<IJiraIssueApi>(msSettings.EndPoint);
            }

            var value = Convert.ToBase64String(Encoding.ASCII.GetBytes(msSettings.Username + ":" + msSettings.Password));
            api.Authorization = new AuthenticationHeaderValue("Basic", value);

            return api;
        }


        /// <summary>
        /// Main entry point of the application.
        /// </summary>
        /// <param name="args">The program arguments.</param>
        static void Main(string[] args)
        {
            try
            {
                msSettings = TechSettings.Build(@".\techsettings.json");
                //using (var reader = new CsvReader(new ExcelParser(@"d:\temp\TEMPLATE_CREATION_JIRA.xlsx")))
                //{
                //    var issues = reader.GetRecords<XlsIssue>().Where(issue => issue.IsEmpty == false).ToList();
                //    Console.WriteLine(issues.Count());
                //    foreach (var issue in issues)
                //    {
                //        if (string.IsNullOrEmpty(issue.XlsId))
                //        {
                //            issue.IsValid = false;
                //            issue.Errors = new StringBuilder(issue.Errors).AppendLine("XLS ID cannot be null").ToString();
                //        }
                //        else
                //        {
                //            // If creation or update ?
                //            if (string.IsNullOrEmpty(issue.Id))
                //            {
                //                Console.WriteLine("Create issue : " + issue.XlsId);
                //                if (string.IsNullOrEmpty(issue.ParentId) == false)
                //                {
                //                    try
                //                    {
                //                        GetApi().GetIssue(issue.ParentId).Wait();
                //                    }
                //                    catch (Exception ex)
                //                    {
                //                        issue.IsValid = false;
                //                        issue.Errors = new StringBuilder(issue.Errors).AppendLine("Parent not found").ToString();
                //                    }

                //                }
                //                // Check mandatory fields.
                //            }
                //            else
                //            {
                //                Console.WriteLine("Update issue : " + issue.Id);
                //                // Check mandatory fields.
                //            }
                //        }                        
                //    }
                //}

                msCustomFields = GetApi().GetFields().Result.ToList();
                foreach (var customField in msCustomFields)
                {
                    Console.WriteLine(customField.ToString());
                }

                try
                {
                    CreateEpic("my summary", "'my epic name", "my description", "my criteria", "Medium", "NBY", "NBY-1");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }


            }
                catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
    }
}