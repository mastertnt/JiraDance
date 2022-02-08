using System.Net.Http.Headers;
using System.Text;
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
                string endpoint = "https://nby.atlassian.net/rest";
                string user = "nby.dev@gmail.com";
                string password = "7F8xeNP5Qfp0415fvxRp7DF0";

                // Create an implementation of that interface
                // We'll pass in the base URL for the API
                IJiraIssueApi api = RestClient.For<IJiraIssueApi>(endpoint, new DebugResponseDeserializer());

                var value = Convert.ToBase64String(Encoding.ASCII.GetBytes(user + ":" + password));
                api.Authorization = new AuthenticationHeaderValue("Basic", value);
                
                Console.WriteLine(api.GetAllIssuesByProject("nby").Result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
    }
}