using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            const int port = 8080;
            string uri = string.Format("http://localhost:{0}", port);
            
            // Self-host api. Press any key to exit.
            using (WebApp.Start<Startup>(uri))
            {
                Console.WriteLine(string.Format("Api in ready on: {0}", uri));
                Console.ReadKey();
            }
        }
    }
}
