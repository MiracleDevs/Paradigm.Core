/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

using System;

namespace Paradigm.Core.DependencyInjection.Interfaces
{
    public interface IDependencyContainer: IDependencyScope
    {
        TInternalContainer GetInternalContainer<TInternalContainer>() where TInternalContainer : class;

        IDependencyScope CreateScope(params object[] parameters);

        bool IsRegistered<TInterface>();

        bool IsRegistered(Type type);
    }
}