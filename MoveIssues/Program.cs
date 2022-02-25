using AtlassianCore;
using AtlassianCore.Models;
using AtlassianCore.Utility;
using RestEase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace MoveIssues
{
    class Program
    {
        /// <summary>
        /// Current NLog.
        /// </summary>
        private static readonly NLog.Logger msLogger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Gets all issues.
        /// </summary>
        private static ObservableRangeCollection<JiraIssueSmall> msIssues;

        /// <summary>
        /// Stores the technical settings.
        /// </summary>
        private static TechSettings settings;

        /// <summary>
        /// Stores the custom fields.
        /// </summary>
        private static List<JiraCustomField> msCustomFields;

        static void Main(string[] args)
        {
            settings = TechSettings.Build(@".\techsettings.json");

           


            // NBY-S-1
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

            // First, read the sprints and find all issues with stories in another sprint or in backlog.
            msIssues = new ObservableRangeCollection<JiraIssueSmall>();

            int before = 0;
            int after = 50;
            int shift = 0;
            while (before + 50 == after)
            {
                before = msIssues.Count;
                msIssues.AddRange(api.ExecuteJqj("sprint = 1 AND resolution = Unresolved", 0).Result.Issues);
                shift += 50;
                after = msIssues.Count;
            }

            // Retrieve custom fields.
            msCustomFields = api.GetFields().Result.ToList();
            foreach (var customField in msCustomFields)
            {
                Console.WriteLine(customField.ToString());
            }

        //    AssemblyName aName = new AssemblyName("DynamicAssemblyExample");
        //AssemblyBuilder ab =
        //    AssemblyBuilder.DefineDynamicAssembly(
        //        aName,
        //        AssemblyBuilderAccess.Run);

        //// The module name is usually the same as the assembly name.
        //ModuleBuilder mb =
        //    ab.DefineDynamicModule(aName.Name, null, Type.GetType("IJiraBuilder");

        //TypeBuilder tb = mb.DefineType(
        //    "MyDynamicType",
        //     TypeAttributes.Public);


            // Now, get the parent of each issue found.
            foreach (var issue in msIssues)
            {
                JiraIssue fullIssue = api.GetIssue(issue.Key).Result;
                if (fullIssue.Type == "Tâche")
                { 
                    JiraIssueLink foundParentLink = fullIssue.Issuelinks.FirstOrDefault(link => link.OutwardType == "relates to");
                    if (foundParentLink != null)
                    {
                        Console.WriteLine("Parent is " + foundParentLink.InwardKey);
                        JiraIssueSmall.CustomFields.Add("fields." + msCustomFields.FirstOrDefault(cf => cf.Name == "Sprint").Id, "");
                        JiraIssueSmall parent = api.GetSmallIssue(foundParentLink.InwardKey).Result;
                        Console.WriteLine(JiraIssueSmall.CustomFields["fields." + msCustomFields.FirstOrDefault(cf => cf.Name == "Sprint").Id]);
                    }
                }
                else if (fullIssue.Type == "Story")
                {

                }
                
                Console.WriteLine(fullIssue);
            }

            //Issue toCheckapi.GetIssue("NBY-74");

        }
    }
}
