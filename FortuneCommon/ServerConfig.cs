using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Steeltoe.Extensions.Configuration;
using PA = Microsoft.Extensions.PlatformAbstractions;
namespace FortuneCommon
{
    public static class ServerConfig
    {

        public static IConfigurationRoot Configuration { get; set; }

        public static void RegisterConfig(string environment, Action<HostingEnvironment, ConfigurationBuilder> configurationBuilder)
        {
            var env = new HostingEnvironment(environment);
            // Set up configuration sources.
            var builder = new ConfigurationBuilder();
            builder.AddCloudFoundry();
            configurationBuilder(env, builder);

            Configuration = builder.Build();

            // setup period refresh of configuration
            var myTimer = new System.Timers.Timer();
            myTimer.Elapsed += (sender, args) => Configuration.Reload();
            myTimer.Interval = 10000;
            myTimer.Enabled = true;
        }
    }

    public class HostingEnvironment : IHostingEnvironment
    {
        public HostingEnvironment(string env)
        {
            EnvironmentName = env;

            ApplicationName = PA.PlatformServices.Default.Application.ApplicationName;
            ContentRootPath = PA.PlatformServices.Default.Application.ApplicationBasePath;
        }

        public string ApplicationName { get; set; }

        public IFileProvider ContentRootFileProvider { get; set; }

        public string ContentRootPath { get; set; }

        public string EnvironmentName { get; set; }
        public object PlatformServices { get; private set; }
        public IFileProvider WebRootFileProvider { get; set; }

        public string WebRootPath { get; set; }

        IFileProvider IHostingEnvironment.WebRootFileProvider { get; set; }
    }
}
