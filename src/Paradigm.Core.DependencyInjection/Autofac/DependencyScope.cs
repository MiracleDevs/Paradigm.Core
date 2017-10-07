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
    internal class DependencyScope : IDependencyScope
    {
        #region Properties

        private ILifetimeScope Scope { get; }

        #endregion

        #region Constructor

        internal DependencyScope(ILifetimeScope scope)
        {
            this.Scope = scope;
        }

        #endregion

        #region Public Methods

        public TInternalScope GetInternalScope<TInternalScope>() where TInternalScope : class
        {
            return this as TInternalScope;
        }

        public TInterface Resolve<TInterface>(params IResolutionParameter[] parameters)
        {
            if (this.Scope == null)
            {
                throw new Exception("The Dependency Container has not been built.");
            }

            return parameters == null
                ? this.Scope.Resolve<TInterface>()
                : this.Scope.Resolve<TInterface>(parameters.Select(x => new NamedParameter(x.Name, x.Value)));
        }

        public object Resolve(Type type, params IResolutionParameter[] parameters)
        {
            if (this.Scope == null)
            {
                throw new Exception("The Dependency Container has not been built.");
            }

            return parameters == null
                ? this.Scope.Resolve(type)
                : this.Scope.Resolve(type, parameters.Select(x => new NamedParameter(x.Name, x.Value)));
        }

        public IDependencyScope CreateScope(params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public bool IsRegistered<TInterface>()
        {
            return this.Scope.IsRegistered<TInterface>();
        }

        public bool IsRegistered(Type type)
        {
            return this.Scope.IsRegistered(type);
        }

        public void Dispose()
        {
            this.Scope.Dispose();
        }

        #endregion
    }
}