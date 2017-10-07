/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

using System;

namespace Paradigm.Core.Mapping.Interfaces
{
    public interface IMapper
    {
        bool MapExists(Type sourceType, Type destinationType);

        IMemberConfiguration<TSource, TDestination> Register<TSource, TDestination>();

        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);

        TDestination Map<TSource, TDestination>(TSource source);

        TDestination Map<TDestination>(object source);

        object Map(object source, object destination, Type sourceType, Type destinationType);

        object Map(object source, Type sourceType, Type destinationType);

        void Compile();

        void Reset();
    }
}