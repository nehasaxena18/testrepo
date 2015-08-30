using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Configuration;
using System.Net.Http;

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
                var pathroot = server.Configuration.VirtualPathRoot;
                
                Console.WriteLine("Listening for HTTP requests.");
                Console.WriteLine(pathroot.ToString());
                Console.WriteLine("(Run the ClientApp project to send requests).");
                Console.WriteLine();

             

                Console.WriteLine("Press Enter to quit.");


                Console.WriteLine("(Runinng the ClientApp,Getting all products).");
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["applicationuri"]);

                HttpResponseMessage resp = client.GetAsync("api/products").Result;
                resp.EnsureSuccessStatusCode();

                var products = resp.Content.ReadAsAsync<IEnumerable<SelfHost.Product>>().Result;
                foreach (var p in products)
                {
                    Console.WriteLine("{0} {1} {2} ({3})", p.Id, p.Name, p.Price, p.Category);
                }
                Console.ReadLine();
            }
        }
    }
}
