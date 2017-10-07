/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

using System;
using AutoMapper;
using Paradigm.Core.Mapping.Interfaces;

namespace Paradigm.Core.Mapping.AutoMapper
{
    internal class Mapper : Interfaces.IMapper
    {
        private InternalProfile InternalProfile { get; set; }

        private MapperConfiguration Configuration { get; set; }

        private global::AutoMapper.IMapper InternalMapper { get; set; }

        public Mapper()
        {
            this.InternalProfile = new InternalProfile();
        }

        public bool MapExists(Type sourceType, Type destinationType)
        { 
            return this.InternalProfile.MapExists(sourceType, destinationType);
        }

        public IMemberConfiguration<TSource, TDestination> Register<TSource, TDestination>()
        {
            return new MemberConfiguration<TSource, TDestination>(this.InternalProfile.CreateMap<TSource, TDestination>());
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return this.InternalMapper.Map(source, destination);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return this.InternalMapper.Map<TSource, TDestination>(source);
        }

        public TDestination Map<TDestination>(object source)
        {
            return this.InternalMapper.Map<TDestination>(source);
        }

        public object Map(object source, object destination, Type sourceType, Type destinationType)
        {
            return this.InternalMapper.Map(source, destination, sourceType, destinationType);
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            return this.InternalMapper.Map(source, sourceType, destinationType);
        }

        public void Compile()
        {
            this.Configuration = new MapperConfiguration(cfg => cfg.AddProfile(this.InternalProfile));
            this.Configuration.AssertConfigurationIsValid();
            this.Configuration.CompileMappings();
            this.InternalMapper = this.Configuration.CreateMapper();
        }

        public void Reset()
        {
            this.InternalProfile = new InternalProfile();
        }
    }
}