/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

using System;
using Paradigm.Core.Mapping.Interfaces;

namespace Paradigm.Core.Mapping
{
    internal static class MapperFactory
    {
        internal static IMapper Create(MapperLibrary library)
        {
            switch (library)
            {
                case MapperLibrary.AutoMapper:
                    return new AutoMapper.Mapper();

                case MapperLibrary.ExpressMapper:
                    throw new Exception("Waiting for .NET Standard support form ExpressMapper.");

                default:
                    throw new ArgumentOutOfRangeException(nameof(library), library, "Mapping Library not recognized.");
            }
        }
    }
}