using System;

namespace Paradigm.Core.Tests.Fixtures.Mappings
{
    public interface IMappingObject
    {
        int Id { get; set; }

        string Name { get; set; }
    }

    public class ComplexMappingObject: IMappingObject
    {
        private IServiceProvider ServiceProvider { get; }

        public int Id { get; set; }

        public string Name { get; set; }

        public ComplexMappingObject(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }
    }

    public class SimpleMappingObject: IMappingObject
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}