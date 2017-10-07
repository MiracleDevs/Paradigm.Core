/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

using Paradigm.Core.Mapping.Interfaces;

namespace Paradigm.Core.Mapping
{
    public static class Mapper
    {
        private static readonly object Padlock = new object();

        public static IMapper Container { get; private set; }

        public static void Initialize(MapperLibrary library)
        {
            lock (Padlock)
            {
                Container = MapperFactory.Create(library);
            }
        }

        public static void Compile()
        {
            lock (Padlock)
            {
                Container.Compile();
            }
        }
    }
}