/*!
 * Paradigm Framework - Core Libraries
 * Copyright (c) 2017 Miracle Devs, Inc
 * Licensed under MIT (https://github.com/MiracleDevs/Paradigm.Core/blob/master/LICENSE)
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Paradigm.Core.Assemblies
{
    /// <inheritdoc />
    /// <summary>
    /// Extends the default <see cref="T:System.Runtime.Loader.AssemblyLoadContext" /> providing custom folders to search the assemblies being loaded.
    /// </summary>
    /// <remarks>
    /// When dinamically loading assemblies, this class provides the means to search in all the specified assemblies.
    /// By default, if the framework can not find the assembly, this class will come in and try to find the assembly
    /// in one of the provided folders.
    /// </remarks>
    /// <seealso cref="T:System.Runtime.Loader.AssemblyLoadContext" />
    public class AssemblyLoader : AssemblyLoadContext
    {
        /// <summary>
        /// Gets the optional directories.
        /// </summary>
        public List<string> OptionalDirectories { get; }

        /// <summary>
        /// Gets a value indicating whether the class should look in the nuget folder.
        /// </summary>
        public bool NugetLookUp { get; }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Paradigm.Core.Assemblies.AssemblyLoader" /> class.
        /// </summary>
        /// <param name="optionalDirectories">The optional directories.</param>
        /// <param name="nugetLookUp">if set to <c>true</c> the system will look inside the global nuget directory.</param>
        public AssemblyLoader(IEnumerable<string> optionalDirectories = null, bool nugetLookUp = false)
        {
            this.OptionalDirectories = optionalDirectories?.ToList() ?? new List<string>();
            this.NugetLookUp = nugetLookUp;
        }

        /// <summary>
        /// Adds an optional directory.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        public void AddOptionalDirectory(string directoryPath)
        {
            if (this.OptionalDirectories.Any(x => x == directoryPath))
                return;

            this.OptionalDirectories.Add(directoryPath);
        }

        /// <inheritdoc />
        /// <summary>
        /// Loads the specified assembly.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <returns>The loaded assembly.</returns>
        protected override Assembly Load(AssemblyName assemblyName)
        {
            return ResolveAssembly(assemblyName, this, this.OptionalDirectories, this.NugetLookUp);
        }

        /// <summary>
        /// Resolves the assembly.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <param name="assemblyLoadContext">The assembly load context.</param>
        /// <param name="optionalDirectories">The optional directories.</param>
        /// <param name="nugetLookUp">if set to <c>true</c> the system will look inside the global nuget directory.</param>
        /// <returns></returns>
        public static Assembly ResolveAssembly(AssemblyName assemblyName, AssemblyLoadContext assemblyLoadContext, IEnumerable<string> optionalDirectories, bool nugetLookUp = false)
        {
            var possibleAssemblies = new List<string>();
            var assemblyFileName = $"{assemblyName.Name}.dll";
            var nugetPath = Path.Combine(Environment.ExpandEnvironmentVariables("%USERPROFILE%"), ".nuget\\packages\\", assemblyName.Name.ToLower());

            foreach (var directory in optionalDirectories)
            {
                if (Directory.Exists(directory))
                {
                    possibleAssemblies.AddRange(Directory.EnumerateFiles(directory, assemblyFileName, SearchOption.AllDirectories));
                }
            }

            if (nugetLookUp && Directory.Exists(nugetPath))
            {
                possibleAssemblies.AddRange(Directory.EnumerateFiles(nugetPath, assemblyFileName, SearchOption.AllDirectories));
            }

            foreach (var assemblyPath in possibleAssemblies)
            {
                try
                {
                    return assemblyLoadContext.LoadFromAssemblyPath(assemblyPath);
                }
                catch
                {
                    // ignored
                }
            }

            return null;
        }
    }
}
