using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Configuration;

namespace SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {


            var uriString = ConfigurationManager.AppSettings["applicationuri"];
            var config = new HttpSelfHostConfiguration(uriString);

            config.Routes.MapHttpRoute(
                "API Default", "api/{controller}/{id}",
                new { id = RouteParameter.Optional });

            using (HttpSelfHostServer server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();

                Console.WriteLine("Listening for HTTP requests.");
                Console.WriteLine("(Run the ClientApp project to send requests).");
                Console.WriteLine();
                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }
        }
    }
}
