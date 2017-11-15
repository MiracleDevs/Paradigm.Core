/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

using System;
using System.Linq.Expressions;
using AutoMapper;
using Paradigm.Core.Mapping.Interfaces;

namespace Paradigm.Core.Mapping.AutoMapper
{
    internal class MemberConfiguration<TSource, TDestination> : IMemberConfiguration<TSource, TDestination>
    {
        private IMappingExpression<TSource, TDestination> Configuration { get; }

        internal MemberConfiguration(IMappingExpression<TSource, TDestination> configuration)
        {
            this.Configuration = configuration;
        }

        public IMemberConfiguration<TSource, TDestination> Ignore<TMember>(Expression<Func<TDestination, TMember>> destinationMember)
        {
            this.Configuration.ForMember(destinationMember, x => x.Ignore());
            return this;
        }

        public IMemberConfiguration<TSource, TDestination> Membmer<TMember>(Expression<Func<TDestination, TMember>> destinationMember, Expression<Func<TSource, TMember>> sourceMember)
        {
            this.Configuration.ForMember(destinationMember, x => x.MapFrom(sourceMember));
            return this;
        }

        public IMemberConfiguration<TSource, TDestination> Function<TMember>(Expression<Func<TDestination, TMember>> destinationMember, Expression<Func<TSource, TMember>> sourceMember)
        {
            this.Configuration.ForMember(destinationMember, x => x.MapFrom(sourceMember));
            return this;
        }

        public IMemberConfiguration<TSource, TDestination> ConstructUsing(Func<TSource, TDestination> constructor)
        {
            this.Configuration.ConstructUsing(constructor);
            return this;
        }
    }
}