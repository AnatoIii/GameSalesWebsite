using System;
using Xunit;
using ApplicationBlocks.IoC;
using ApplicationBlocks.IoC.Extensions;
using FluentAssertions;
using System.Collections.Generic;

namespace ApplicationBlocksTest.IoC
{
    public class ContainerTest
    {
        #region Test data

        interface IFoo
        { }

        class Foo : IFoo
        { }

        interface IBar
        { }

        class Bar : IBar
        {
            public IFoo Foo { get; set; }

            public Bar(IFoo foo)
            {
                Foo = foo;
            }
        }

        interface IBaz
        { }

        class Baz : IBaz
        {
            public IFoo Foo { get; set; }
            public IBar Bar { get; set; }

            public Baz(IFoo foo, IBar bar)
            {
                Foo = foo;
                Bar = bar;
            }
        }

        class SpyDisposable : IDisposable
        {
            public bool Disposed { get; private set; }

            public void Dispose() => Disposed = true;
        }

        #endregion

        private Container _container { get; set; }

        public ContainerTest()
        {
            _container = new Container();
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Container")]
        public void Simple_Reflection_Construction_Works_Fine()
        {
            _container.Register<IFoo>(typeof(Foo));

            object instance = _container.Resolve<IFoo>();

            // Instance should be of the registered type 
            instance.Should().BeNull();
            instance.Should().BeOfType(typeof(Foo));
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Container")]
        public void Recursive_Reflection_Construction_Works_Fine()
        {
            _container.Register<IFoo>(typeof(Foo));
            _container.Register<IBar>(typeof(Bar));
            _container.Register<IBaz>(typeof(Baz));

            IBaz instance = _container.Resolve<IBaz>();

            // Test that the correct types were created
            instance.Should().BeOfType(typeof(Baz));

            var baz = instance as Baz;
            baz.Bar.Should().BeOfType(typeof(Bar));
            baz.Foo.Should().BeOfType(typeof(Foo));
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Container")]
        public void Simple_Factory_Construction_Works_Fine()
        {
            _container.Register<IFoo>(() => new Foo());

            object instance = _container.Resolve<IFoo>();

            // Instance should be of the registered type 
            instance.Should().BeOfType(typeof(Foo));
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Container")]
        public void Mixed_Construction_On_Recursive_Construction_Works_Fine()
        {
            _container.Register<IFoo>(() => new Foo());
            _container.Register<IBar>(typeof(Bar));
            _container.Register<IBaz>(typeof(Baz));

            IBaz instance = _container.Resolve<IBaz>();

            // Test that the correct types were created
            instance.Should().BeOfType(typeof(Baz));

            var baz = instance as Baz;
            baz.Bar.Should().BeOfType(typeof(Bar));
            baz.Foo.Should().BeOfType(typeof(Foo));
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Container")]
        public void Instance_Resolution_Works_Fine()
        {
            _container.Register<IFoo>(typeof(Foo));

            object instance1 = _container.Resolve<IFoo>();
            object instance2 = _container.Resolve<IFoo>();

            // Instances should be different between calls to Resolve
            instance1.Should().BeOfType(typeof(Foo));
            instance2.Should().BeOfType(typeof(Foo));

            instance1.Should().NotBeSameAs(instance2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Container")]
        public void Singleton_Resolution_Works_Fine()
        {
            _container.Register<IFoo>(typeof(Foo)).AsSingleton();

            object instance1 = _container.Resolve<IFoo>();
            object instance2 = _container.Resolve<IFoo>();

            // Instances should be identic between calls to Resolve
            instance1.Should().BeSameAs(instance2);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Container")]
        public void PerScope_Resolution_Works_Fine()
        {
            _container.Register<IFoo>(typeof(Foo)).PerScope();

            object instance1 = _container.Resolve<IFoo>();
            object instance2 = _container.Resolve<IFoo>();

            // Instances should be same as the container is itself a scope
            instance1.Should().BeSameAs(instance2);

            using (var scope = _container.CreateScope())
            {
                object instance3 = scope.Resolve<IFoo>();
                object instance4 = scope.Resolve<IFoo>();

                // Instances should be equal inside a scope
                instance3.Should().BeSameAs(instance4);

                // Instances should not be equal between scopes
                instance1.Should().NotBeSameAs(instance3);
            }
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Container")]
        public void Mixed_Scope_Resolution_Works_Fine()
        {
            _container.Register<IFoo>(typeof(Foo)).PerScope();
            _container.Register<IBar>(typeof(Bar)).AsSingleton();
            _container.Register<IBaz>(typeof(Baz));

            using (var scope = _container.CreateScope())
            {
                Baz instance1 = scope.Resolve<IBaz>() as Baz;
                Baz instance2 = scope.Resolve<IBaz>() as Baz;

                // Ensure resolutions worked as expected
                instance1.Should().NotBeSameAs(instance2);

                // Singleton should be same
                instance1.Bar.Should().BeSameAs(instance2.Bar);
                (instance1.Bar as Bar).Foo.Should().BeSameAs((instance2.Bar as Bar).Foo);

                // Scoped types should be the same
                instance1.Foo.Should().BeSameAs(instance2.Foo);

                // Singleton should not hold scoped object
                instance1.Foo.Should().NotBeSameAs((instance1.Bar as Bar).Foo);
                instance2.Foo.Should().NotBeSameAs((instance2.Bar as Bar).Foo);
            }
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Container")]
        public void Singleton_Scoped_Resolution_Works_Fine()
        {
            _container.Register<IFoo>(typeof(Foo)).AsSingleton();
            _container.Register<IBar>(typeof(Bar)).PerScope();

            var instance1 = _container.Resolve<IBar>();

            using (var scope = _container.CreateScope())
            {
                var instance2 = _container.Resolve<IBar>();

                // Singleton should resolve to the same instance
                (instance1 as Bar).Foo.Should().BeSameAs((instance2 as Bar).Foo);
            }
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Container")]
        public void Mixed_NoScope_Resolution_Works_Fine()
        {
            _container.Register<IFoo>(typeof(Foo)).PerScope();
            _container.Register<IBar>(typeof(Bar)).AsSingleton();
            _container.Register<IBaz>(typeof(Baz));

            Baz instance1 = _container.Resolve<IBaz>() as Baz;
            Baz instance2 = _container.Resolve<IBaz>() as Baz;

            // Ensure resolutions worked as expected
            instance1.Should().NotBeSameAs(instance2);

            // Singleton should be same
            instance1.Bar.Should().BeSameAs(instance2.Bar);

            // Scoped types should not be different outside a scope
            instance1.Foo.Should().BeSameAs(instance2.Foo);
            instance1.Foo.Should().BeSameAs((instance1.Bar as Bar).Foo);
            instance1.Foo.Should().BeSameAs((instance2.Bar as Bar).Foo);
            instance2.Foo.Should().BeSameAs((instance2.Bar as Bar).Foo);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Container")]
        public void Mixed_Order_Of_Registration_Works_Fine()
        {
            _container.Register<IBaz>(typeof(Baz));
            _container.Register<IBar>(typeof(Bar));
            _container.Register<IFoo>(() => new Foo());

            IBaz instance = _container.Resolve<IBaz>();

            // Test that the correct types were created
            instance.Should().BeOfType(typeof(Baz));

            var baz = instance as Baz;
            baz.Bar.Should().BeOfType(typeof(Bar));
            baz.Foo.Should().BeOfType(typeof(Foo));
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Container")]
        public void Scope_Disposes_Of_Cached_Instances_Works_Fine()
        {
            _container.Register<SpyDisposable>(typeof(SpyDisposable)).PerScope();
            SpyDisposable spy;

            using (var scope = _container.CreateScope())
            {
                spy = scope.Resolve<SpyDisposable>();
            }

            spy.Disposed.Should().BeTrue();
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Container")]
        public void Container_Disposes_Of_Singletons_Works_Fine()
        {
            _container.Register<SpyDisposable>().AsSingleton();
            SpyDisposable spy;
            using (var container = _container.CreateScope())
            {
                spy = container.Resolve<SpyDisposable>();
            }

            spy.Disposed.Should().BeFalse();
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Container")]
        public void Container_Disposes_Of_PerScope_Works_Fine()
        {
            _container.Register<SpyDisposable>().PerScope();
            SpyDisposable spy;
            using (var container = _container.CreateScope())
            {
                spy = container.Resolve<SpyDisposable>();
            }

            spy.Disposed.Should().BeTrue();
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Container")]
        public void Container_Disposes_Of_Singletons_In_Scope_Works_Fine()
        {
            SpyDisposable spy;
            using (var container = new Container())
            {
                container.Register<SpyDisposable>().AsSingleton();
                spy = container.Resolve<SpyDisposable>();
            }

            spy.Disposed.Should().BeTrue();
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Container")]
        public void SingletonsAreDifferentAcrossContainers_Works_Fine()
        {
            var container1 = new Container();
            container1.Register<IFoo>(typeof(Foo)).AsSingleton();

            var container2 = new Container();
            container2.Register<IFoo>(typeof(Foo)).AsSingleton();

            container1.Resolve<IFoo>().Should().NotBeSameAs(container2.Resolve<IFoo>());
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Container")]
        public void GetService_Unregistered_Type_Returns_Null_Works_Fine()
        {
            using (var container = new Container())
            {
                object value = container.GetService(typeof(Foo));

                value.Should().BeNull();
            }
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Container")]
        public void GetService_Missing_Dependency_Throws_KeyNotFoundException()
        {
            using (var container = new Container())
            {
                container.Register<Bar>();

                Action act = () => container.GetService(typeof(Bar));

                act.Should().Throw<KeyNotFoundException>();
            }
        }

    }
}
