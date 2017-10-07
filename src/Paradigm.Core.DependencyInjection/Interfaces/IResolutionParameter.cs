/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

namespace Paradigm.Core.DependencyInjection.Interfaces
{
    public interface IResolutionParameter
    {
        string Name { get; }

        object Value { get; }
    }
}