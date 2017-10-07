/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace Paradigm.Core.Mapping.AutoMapper
{
    internal class InternalProfile : Profile
    {
        private List<Tuple<Type, Type>> Mappings { get; }

        public InternalProfile()
        {
            this.Mappings = new List<Tuple<Type, Type>>();
        }

        public new IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>()
        {
            this.Mappings.Add(new Tuple<Type, Type>(typeof(TSource), typeof(TDestination)));
            return base.CreateMap<TSource, TDestination>(MemberList.Source);
        }

        public bool MapExists(Type sourceType, Type destinationType)
        {
            return this.Mappings.Any(x => x.Item1 == sourceType && x.Item2 == destinationType);           
        }
    }
}