using System.Numerics;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paradigm.Core.Mapping;
using Paradigm.Core.Tests.Fixtures.Mappings;

namespace Paradigm.Core.Tests.Mappings
{
    [TestClass]
    public class MappingTest
    {
        [TestMethod]
        public void ShouldCreateMapper()
        {
            Mapper.Initialize(MapperLibrary.AutoMapper);
            Mapper.Container.Should().NotBeNull();
        }

        [TestMethod]
        public void ShouldCompile()
        {
            Mapper.Initialize(MapperLibrary.AutoMapper);
            Mapper.Container.Register<IMappingObject, SimpleMappingObject>();
            Mapper.Compile();
        }

        [TestMethod]
        public void ShouldCompileWithoutParameterlessConstructor()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<ComplexMappingObject>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            Mapper.Initialize(MapperLibrary.AutoMapper);
            Mapper.Container.Register<IMappingObject, SimpleMappingObject>();
            Mapper.Container.Register<IMappingObject, ComplexMappingObject>().ConstructUsing(x => new ComplexMappingObject(serviceProvider));
            Mapper.Compile();
        }

        [TestMethod]
        public void ShouldMap()
        {
            Mapper.Initialize(MapperLibrary.AutoMapper);
            Mapper.Container.Register<IMappingObject, SimpleMappingObject>();
            Mapper.Compile();

            var simpleObject = new SimpleMappingObject { Id = 1, Name = "Peter" };

            var mappedObject = Mapper.Container.Map<SimpleMappingObject>(simpleObject);

            mappedObject.Id.Should().Be(simpleObject.Id);
            mappedObject.Name.Should().Be(simpleObject.Name);
        }

        [TestMethod]
        public void ShouldMapWithoutParameterlessConstructor()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<ComplexMappingObject>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            Mapper.Initialize(MapperLibrary.AutoMapper);
            Mapper.Container.Register<IMappingObject, SimpleMappingObject>();
            Mapper.Container.Register<IMappingObject, ComplexMappingObject>().ConstructUsing(x => new ComplexMappingObject(serviceProvider));
            Mapper.Compile();

            var simpleObject = new SimpleMappingObject { Id = 1, Name = "Peter" };

            var mappedObject = Mapper.Container.Map<ComplexMappingObject>(simpleObject);

            mappedObject.Id.Should().Be(simpleObject.Id);
            mappedObject.Name.Should().Be(simpleObject.Name);
        }
    }
}