 #nullable disable
 using System;
 using System.Collections.Generic;
 using System.Linq;
 using System.Reflection;

 namespace TidyUtility.Core.Extensions
{
    public static class AssemblyExtensions
    {
        public static void EnsureAssemblyWithTypeIsLoaded(this Type type)
        {
            // If the type is referenced, it's assembly is loaded.
        }

        /// <summary>
        /// Gets all Loaded Assemblies that reference the passed in assembly.
        /// </summary>
        /// <param name="assemblyBeingReferenced"></param>
        /// <returns></returns>
        public static HashSet<Assembly> GetAllReferencingAssemblies(this Assembly assemblyBeingReferenced)
        {
            AssemblyName referencedAssemblyName = assemblyBeingReferenced.GetName();
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(asm => asm.GetReferencedAssemblies()
                    .Any(refAsm => AssemblyName.ReferenceMatchesDefinition(refAsm, referencedAssemblyName)))
                .ToHashSet();
        }

        public static IEnumerable<TypeAndAttrib<TAttrib>> GetTypesWithAttributes<TAttrib>(this Assembly assemblyToSearch, 
            bool inherit = false)
            where TAttrib : Attribute
        {
            foreach (Type type in assemblyToSearch.GetTypes())
            {
                IEnumerable<TAttrib> attribs = type
                    .GetCustomAttributes(typeof(TAttrib), false)
                    .Cast<TAttrib>();

                    yield return new TypeAndAttrib<TAttrib>(type, attribs.ToArray());
            }
        }

        public static AssemblyVersionInfo GetVersionInformation(this Assembly assembly)
        {
            return new AssemblyVersionInfo()
            {
                AssemblyName = assembly.GetName().Name ?? string.Empty,
                Version = GetAttribute<AssemblyFileVersionAttribute>()?.Version ?? string.Empty,
                InformationalVersion = GetAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? string.Empty,
                Copyright = GetAttribute<AssemblyCopyrightAttribute>()?.Copyright ?? string.Empty,
            };

            TAttribute GetAttribute<TAttribute>()
                where TAttribute : Attribute
            {
                return assembly.GetCustomAttributes(typeof(TAttribute), false).Cast<TAttribute>().FirstOrDefault();
            }
        }
    }

    public record AssemblyVersionInfo
    {
        public string AssemblyName { get; init; }
        public string Version { get; init; }
        public string InformationalVersion { get; init; }
        public string Copyright { get; init; }
    }

    public class TypeAndAttrib<TAttrib>
        where TAttrib : Attribute
    {
        public TypeAndAttrib(Type type, TAttrib[] attribs)
        {
            this.Type = type;
            this.Attribs = attribs;
        }

        public Type Type { get; }
        public TAttrib[] Attribs { get; }
    }
}
