/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

using System;
using System.Linq.Expressions;

namespace Paradigm.Core.Mapping.Interfaces
{
    public interface IMemberConfiguration
    {

    }

    public interface IMemberConfiguration<TSource, TDestination> : IMemberConfiguration
    {
        IMemberConfiguration<TSource, TDestination> Ignore<TMember>(Expression<Func<TDestination, TMember>> destinationMember);

        IMemberConfiguration<TSource, TDestination> Membmer<TMember>(Expression<Func<TDestination, TMember>> destinationMember, Expression<Func<TSource, TMember>> sourceMember);

        IMemberConfiguration<TSource, TDestination> Function<TMember>(Expression<Func<TDestination, TMember>> destinationMember, Expression<Func<TSource, TMember>> sourceMember);
    }
}