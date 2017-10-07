/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

using Autofac;
using Paradigm.Core.DependencyInjection.Interfaces;

namespace Paradigm.Core.DependencyInjection.Autofac
{
    internal class DependencyBuilder : IDependencyBuilder
    {
        #region Properties

        private ContainerBuilder Builder { get; }

        #endregion

        #region Constructor

        public DependencyBuilder()
        {
            this.Builder = new ContainerBuilder();
        }

        #endregion

        #region Public Methods

        public void Register<TImplementation>() where TImplementation : class
        {
            this.Builder.RegisterType<TImplementation>();
        }

        public void Register<TInterface, TImplementation>() where TImplementation : class, TInterface where TInterface : class
        {
            this.Builder.RegisterType<TImplementation>().As<TInterface>();
        }

        public void RegisterScoped<TImplementation>() where TImplementation: class
        {
            this.Builder.RegisterType<TImplementation>().InstancePerLifetimeScope();
        }

        public void RegisterScoped<TInterface, TImplementation>() where TImplementation : class, TInterface where TInterface: class
        {
            this.Builder.RegisterType<TImplementation>().As<TInterface>().InstancePerLifetimeScope();
        }

        public void RegisterInstance<TInterface, TImplementation>(TImplementation instance) where TImplementation : class, TInterface where TInterface: class
        {
            this.Builder.RegisterInstance(instance).As<TInterface>();
        }

        public void RegisterInstance<TImplementation>(TImplementation instance) where TImplementation : class
        {
            this.Builder.RegisterInstance(instance);
        }

        public IDependencyContainer Build(params object[] parameters)
        {
            return new DependencyContainer(this.Builder.Build());
        }
     
        public void Dispose()
        {
        }

        public TInternalBuilder GetInternalBuilder<TInternalBuilder>() where TInternalBuilder : class
        {
            return this.Builder as TInternalBuilder;
        }

        #endregion
    }
}