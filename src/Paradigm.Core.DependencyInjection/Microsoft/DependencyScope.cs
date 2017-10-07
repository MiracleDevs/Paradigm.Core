/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

using System;
using Microsoft.Extensions.DependencyInjection;
using Paradigm.Core.DependencyInjection.Interfaces;

namespace Paradigm.Core.DependencyInjection.Microsoft
{
    internal class DependencyScope : IDependencyScope
    {
        #region Properties

        private IServiceScope ServiceScope { get;  }

        #endregion

        #region Constructor

        internal DependencyScope(IServiceScope serviceScope)
        {
            this.ServiceScope = serviceScope;
        }

        #endregion

        #region Public Methods

        public void Dispose()
        {
        }

        public TInternalContainer GetInternalScope<TInternalContainer>() where TInternalContainer : class
        {
            return this.ServiceScope as TInternalContainer;
        }

        public TInterface Resolve<TInterface>(params IResolutionParameter[] parameters)
        {
            return this.ServiceScope.ServiceProvider.GetService<TInterface>();
        }

        public object Resolve(Type type, params IResolutionParameter[] parameters)
        {
            return this.ServiceScope.ServiceProvider.GetService(type);
        }

        #endregion
    }
}