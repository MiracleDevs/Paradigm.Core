/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

using System;

namespace Paradigm.Core.DependencyInjection.Interfaces
{
    public interface IDependencyBuilder: IDisposable
    {
        TInternalBuilder GetInternalBuilder<TInternalBuilder>() where TInternalBuilder : class;

        void Register<TImplementation>() where TImplementation : class;

        void Register<TInterface, TImplementation>() where TImplementation : class, TInterface where TInterface : class;


        void RegisterScoped<TImplementation>() where TImplementation : class;

        void RegisterScoped<TInterface, TImplementation>() where TImplementation : class, TInterface where TInterface : class;


        void RegisterInstance<TInterface, TImplementation>(TImplementation instance) where TImplementation : class, TInterface where TInterface : class;

        void RegisterInstance<TImplementation>(TImplementation instance) where TImplementation : class;


        IDependencyContainer Build(params object[] parameters);
    }
}