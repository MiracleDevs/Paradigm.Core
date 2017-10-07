/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Paradigm.Core.DependencyInjection.Interfaces;

namespace Paradigm.Core.DependencyInjection.Microsoft
{
    internal class DependencyContainer : IDependencyContainer
    {
        #region Properties

        private IServiceProvider ServiceProvider { get; }

        private IServiceCollection ServiceServiceCollection { get; }

        #endregion

        #region Constructor

        internal DependencyContainer(IServiceProvider serviceProvider, IServiceCollection serviceCollection)
        {
            this.ServiceProvider = serviceProvider;
            this.ServiceServiceCollection = serviceCollection;
        }

        #endregion

        #region Public Methods

        public void Dispose()
        {
        }

        public TInternalContainer GetInternalContainer<TInternalContainer>() where TInternalContainer : class
        {
            return this.ServiceProvider as TInternalContainer;
        }

        public TInternalContainer GetInternalScope<TInternalContainer>() where TInternalContainer : class
        {
            return this as TInternalContainer;
        }

        public IDependencyScope CreateScope(params object[] parameters)
        {
            return new DependencyScope(this.ServiceProvider.CreateScope());
        }

        public TInterface Resolve<TInterface>(params IResolutionParameter[] parameters)
        {
            return this.ServiceProvider.GetService<TInterface>();
        }

        public object Resolve(Type type, params IResolutionParameter[] parameters)
        {
            return this.ServiceProvider.GetService(type);
        }

        public bool IsRegistered<TInterface>()
        {
            return this.ServiceServiceCollection.Any(x => x.ServiceType == typeof(TInterface));
        }

        public bool IsRegistered(Type type)
        {
            return this.ServiceServiceCollection.Any(x => x.ServiceType == type);
        }

        #endregion
    }
}