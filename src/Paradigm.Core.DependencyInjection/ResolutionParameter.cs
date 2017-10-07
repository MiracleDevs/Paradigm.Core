/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

using Paradigm.Core.DependencyInjection.Interfaces;

namespace Paradigm.Core.DependencyInjection
{
    public class ResolutionParameter : IResolutionParameter
    {
        public string Name { get; }

        public object Value { get; }

        public ResolutionParameter(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}