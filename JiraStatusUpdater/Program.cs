using AtlassianCore.FieldManagement;

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
                Processor processor = new Processor();
                processor.Initialize();
                processor.Run();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
    }
}