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
    internal class MemberConfiguration<TSource, TDestionation> : IMemberConfiguration<TSource, TDestionation>
    {
        private IMappingExpression<TSource, TDestionation> Configuration { get; }

        internal MemberConfiguration(IMappingExpression<TSource, TDestionation> configuration)
        {
            this.Configuration = configuration;
        }

        public IMemberConfiguration<TSource, TDestionation> Ignore<TMember>(Expression<Func<TDestionation, TMember>> destinationMember)
        {
            this.Configuration.ForMember(destinationMember, x => x.Ignore());
            return this;
        }

        public IMemberConfiguration<TSource, TDestionation> Membmer<TMember>(Expression<Func<TDestionation, TMember>> destinationMember, Expression<Func<TSource, TMember>> sourceMember)
        {
            this.Configuration.ForMember(destinationMember, x => x.MapFrom(sourceMember));
            return this;
        }

        public IMemberConfiguration<TSource, TDestionation> Function<TMember>(Expression<Func<TDestionation, TMember>> destinationMember, Expression<Func<TSource, TMember>> sourceMember)
        {
            this.Configuration.ForMember(destinationMember, x => x.MapFrom(sourceMember));
            return this;
        }
    }
}