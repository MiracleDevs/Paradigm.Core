/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

using System;

namespace Paradigm.Core.DependencyInjection.Interfaces
{
    public interface IDependencyScope: IDisposable
    {
        TInternalContainer GetInternalScope<TInternalContainer>() where TInternalContainer : class;

        TInterface Resolve<TInterface>(params IResolutionParameter[] parameters);

        object Resolve(Type type, params IResolutionParameter[] parameters);
    }
}