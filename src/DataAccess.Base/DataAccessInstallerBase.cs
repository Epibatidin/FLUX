﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DataAccess.Interfaces;
using Facade.Configuration;

namespace DataAccess.Base
{
    public abstract class DataAccessInstallerBase<TConfigurationSection> : IDataAccessInstaller where TConfigurationSection : class , IVirtualFileRootConfiguration
    {
        public IConfigurationBinderFacade ConfigurationBinder { get; set; }

        public void Configure(IConfiguration configuration, string sectionName, IServiceCollection services)
        {
            var config = ConfigurationBinder.Bind<TConfigurationSection>(configuration, sectionName);
            config.ID = sectionName;

            services.Add(new ServiceDescriptor(typeof(TConfigurationSection), config));
            services.Add(new ServiceDescriptor(typeof(IVirtualFileRootConfiguration), config));

            RegisterServices(services);
        }
        
        public abstract void RegisterServices(IServiceCollection serviceCollection);
    }
}