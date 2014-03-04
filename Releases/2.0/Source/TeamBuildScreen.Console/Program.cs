
using TeamBuildScreen.Server;

namespace TeamBuildScreen.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new HttpListenerHostWithConfiguration { Configuration = new Configurator() };
            host.Initialize(new[] { "http://+:9222/" }, "/", null);

            host.StartListening();
            System.Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
            host.StopListening();
            host.Close();
        }
    }
}