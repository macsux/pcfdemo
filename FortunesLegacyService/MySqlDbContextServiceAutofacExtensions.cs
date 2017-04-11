using System.Data.Entity;
using Autofac;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Steeltoe.CloudFoundry.Connector;
using Steeltoe.CloudFoundry.Connector.MySql;
using Steeltoe.CloudFoundry.Connector.MySql.EF6;
using Steeltoe.CloudFoundry.Connector.Services;

namespace FortunesLegacyService
{
    public static class MySqlDbContextServiceAutofacExtensions
    {


        public static void AddDbContext<TContext>(this ContainerBuilder services, IConfiguration config) where TContext : DbContext
        {
            MySqlServiceInfo info = config.GetSingletonServiceInfo<MySqlServiceInfo>();
            MySqlProviderConnectorOptions mySqlConfig = new MySqlProviderConnectorOptions(config);
            MySqlDbContextConnectorFactory factory = new MySqlDbContextConnectorFactory(info, mySqlConfig, typeof(TContext));
            services.Register(c => (TContext)factory.Create(null)).InstancePerLifetimeScope();

        }
        public static void RegisterMySqlConnection(this ContainerBuilder container, IConfigurationRoot config)
        {
            MySqlProviderConnectorOptions mySqlConfig = new MySqlProviderConnectorOptions(config);
            MySqlServiceInfo info = config.GetSingletonServiceInfo<MySqlServiceInfo>();
            MySqlProviderConnectorFactory factory = new MySqlProviderConnectorFactory(info, mySqlConfig);
            container.Register<MySqlConnection>(c => (MySqlConnection)factory.Create(null)).InstancePerLifetimeScope();

        }
    }
}