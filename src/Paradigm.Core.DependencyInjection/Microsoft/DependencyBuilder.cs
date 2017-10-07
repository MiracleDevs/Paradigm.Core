/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

using Microsoft.Extensions.DependencyInjection;
using Paradigm.Core.DependencyInjection.Interfaces;

namespace Paradigm.Core.DependencyInjection.Microsoft
{
    internal class DependencyBuilder : IDependencyBuilder
    {
        #region Properties

        private IServiceCollection ServiceCollection { get; }

        #endregion

        #region Constructor

        public DependencyBuilder()
        {
            this.ServiceCollection = new ServiceCollection();
        }

        public DependencyBuilder(IServiceCollection serviceCollection)
        {
            this.ServiceCollection = serviceCollection;
        }

        #endregion

        #region Public Methods

        public void Dispose()
        {
        }

        public IDependencyContainer Build(params object[] parameters)
        {
            return new DependencyContainer(this.ServiceCollection.BuildServiceProvider(), this.ServiceCollection);
        }

        public TInternalBuilder GetInternalBuilder<TInternalBuilder>() where TInternalBuilder : class
        {
            return this.ServiceCollection as TInternalBuilder;
        }

        public void Register<TImplementation>() where TImplementation : class
        {
            this.ServiceCollection.AddTransient<TImplementation, TImplementation>();
        }

        public void Register<TInterface, TImplementation>() where TImplementation : class, TInterface where TInterface : class
        {
            this.ServiceCollection.AddTransient<TInterface, TImplementation>();
        }

        public void RegisterScoped<TImplementation>() where TImplementation : class
        {
            this.ServiceCollection.AddScoped<TImplementation, TImplementation>();
        }

        public void RegisterScoped<TInterface, TImplementation>() where TImplementation : class, TInterface where TInterface : class
        {
            this.ServiceCollection.AddScoped<TInterface, TImplementation>();
        }

        public void RegisterInstance<TInterface, TImplementation>(TImplementation instance) where TImplementation : class, TInterface where TInterface : class
        {
            this.ServiceCollection.AddSingleton<TInterface>(instance);
        }

        public void RegisterInstance<TImplementation>(TImplementation instance) where TImplementation : class
        {
            this.ServiceCollection.AddSingleton(instance);
        }

        #endregion
    }
}