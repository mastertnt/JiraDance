using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using AtlassianCore;
using AtlassianCore.Models;
using Newtonsoft.Json.Converters;
using RestEase;
namespace JiraDance
{
    internal class Program
    {
        /// <summary>
        /// Main entry point of the application.
        /// </summary>
        /// <param name="args">The program arguments.</param>
        static void Main(string[] args)
        {
            try
            {
                TechSettings settings = TechSettings.Build(@".\techsettings.json");
                JiraIssueDatabase database = new JiraIssueDatabase();
                database.Initialize(settings.EndPoint, settings.Username, settings.Password, settings.ResponseDebugPath, settings.ProjectKeys); ;
                database.ExportToExcel(@".\test.xlsx");
                //Process.Start("test.xlsx");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
    }
}