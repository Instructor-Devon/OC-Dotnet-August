using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EFIntro.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EFIntro
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int x;
            object[] anythingArray = new object[10];
            anythingArray[0] = 2;
            anythingArray[1] = 24;
            string unboxed;
            if(anythingArray[0] is string)
                unboxed = ((string)(anythingArray[0]));
            else
                unboxed = "default string";


            int lengthOfBoxed = unboxed.Length;

            List<object> anythings = new List<object>();
            anythings.Add(2);
            anythings.Add(false);
            anythings.Add("Michelle");


            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
