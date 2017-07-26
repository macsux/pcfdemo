using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Integration.WebApi;
using FortuneCommon;
using FortuneCookieDatabase;
using FortunesServicesOwin.Collapsers;
using FortunesServicesOwin.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using Pivotal.Discovery.Client;
using Steeltoe.CircuitBreaker.Hystrix;
using Steeltoe.CircuitBreaker.Hystrix.Strategy.Concurrency;
using Steeltoe.CloudFoundry.Connector.MySql.EF6;
using Steeltoe.Extensions.Configuration;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

[assembly: OwinStartup(typeof(FortunesServicesOwin.Startup))]
namespace FortunesServicesOwin
{
    
    public class Startup
    {

        public void Configuration(IAppBuilder appBuilder)
        {
            try
            {

                ServerConfig.RegisterConfig("development", (env, configBuilder) =>
                    configBuilder.SetBasePath(env.ContentRootPath)
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                        .AddCloudFoundry()
                        .AddEnvironmentVariables());

                // register DI
                var services = new ServiceCollection();
                services.AddDbContext<FortuneCookieDbContext>(ServerConfig.Configuration);
                services.AddDiscoveryClient(ServerConfig.Configuration);

                var builder = new ContainerBuilder();
                builder.Populate(services);
                builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
                builder.RegisterType<TestRequestCollapser>().AsSelf();
                builder.RegisterType<GetFortuneCookiesCommand>().AsSelf();
                var container = builder.Build();
                
                // assign autofac to provide dependency injection on controllers
                appBuilder.UseAutofacMiddleware(container);

                // Configure Web API for self-host. 
                HttpConfiguration config = new HttpConfiguration();
                // default to json
                config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{action}"
                );

                config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
                appBuilder.Use(new Func<AppFunc, AppFunc>(next => (async env =>
                {
                    var hystrix = HystrixRequestContext.InitializeContext();
                    await next(env);
                    hystrix.Dispose();
                })));
                appBuilder.UseWebApi(config);
                appBuilder.UseCors(CorsOptions.AllowAll);
                appBuilder.Use<HystrixRequestContextMiddlewareOwin>();
                // ensure that discovery client is started
                container.Resolve<IDiscoveryClient>();


            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                throw;
            }
        }
    }

    public class HystrixRequestContextMiddlewareOwin
    {
        private readonly Func<IDictionary<string, object>, Task> _next;

        public HystrixRequestContextMiddlewareOwin(Func<IDictionary<string, object>, Task> next)
        {
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            var hystrix = HystrixRequestContext.InitializeContext();
            await _next(environment);
            hystrix.Dispose();
        }
    }
}
