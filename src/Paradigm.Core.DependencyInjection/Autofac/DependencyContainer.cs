/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

using System;
using System.Linq;
using Autofac;
using Paradigm.Core.DependencyInjection.Interfaces;

namespace Paradigm.Core.DependencyInjection.Autofac
{
    internal class DependencyContainer : IDependencyContainer
    {
        #region Properties

        private IContainer Container { get; }

        #endregion

        #region Constructor

        internal DependencyContainer(IContainer container)
        {
            this.Container = container;
        }

        #endregion

        #region Public Methods

        public TInternalContainer GetInternalContainer<TInternalContainer>() where TInternalContainer : class
        {
            return this as TInternalContainer;
        }

        public TInternalScope GetInternalScope<TInternalScope>() where TInternalScope : class
        {
            return this as TInternalScope;
        }

        public TInterface Resolve<TInterface>(params IResolutionParameter[] parameters)
        {
            if (this.Container == null)
            {
                throw new Exception("The Dependency Container has not been built.");
            }

            return parameters == null
                ? this.Container.Resolve<TInterface>()
                : this.Container.Resolve<TInterface>(parameters.Select(x => new NamedParameter(x.Name, x.Value)));
        }

        public object Resolve(Type type, params IResolutionParameter[] parameters)
        {
            if (this.Container == null)
            {
                throw new Exception("The Dependency Container has not been built.");
            }

            return parameters == null
                ? this.Container.Resolve(type)
                : this.Container.Resolve(type, parameters.Select(x => new NamedParameter(x.Name, x.Value)));
        }

        public IDependencyScope CreateScope(params object[] parameters)
        {
            return parameters.Any()
                ? new DependencyScope(this.Container.BeginLifetimeScope(parameters[0]))
                : new DependencyScope(this.Container.BeginLifetimeScope());
        }

        public bool IsRegistered<TInterface>()
        {
            return this.Container.IsRegistered<TInterface>();
        }

        public bool IsRegistered(Type type)
        {
            return this.Container.IsRegistered(type);
        }

        public void Dispose()
        {
            this.Container.Dispose();
        }

        #endregion
    }
}