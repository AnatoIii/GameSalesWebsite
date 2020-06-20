using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ApplicationBlocks.IoC
{
    /// <summary>
    /// Inversion of control container handles dependency injection for registered types
    /// </summary>
    public class Container : IScope
    {
        /// <summary>
        /// Map of registered types
        /// </summary>
        private readonly Dictionary<Type, Func<ILifetime, object>> _registeredTypes = new Dictionary<Type, Func<ILifetime, object>>();

        /// <summary>
        /// Lifetime management
        /// </summary>
        private readonly ContainerLifetime _lifetime;

        /// <summary>
        /// Creates a new instance of IoC Container
        /// </summary>
        public Container() => _lifetime = new ContainerLifetime(t => _registeredTypes[t]);

        /// <summary>
        /// Registers a factory function which will be called to resolve the specified interface
        /// </summary>
        /// <param name="interface">Interface to register</param>
        /// <param name="factory">Factory function</param>
        /// <returns></returns>
        public IRegisteredType Register(Type @interface, Func<object> factory)
            => RegisterType(@interface, _ => factory());

        /// <summary>
        /// Registers an implementation type for the specified interface
        /// </summary>
        /// <param name="interface">Interface to register</param>
        /// <param name="implementation">Implementing type</param>
        /// <returns></returns>
        public IRegisteredType Register(Type @interface, Type implementation)
            => RegisterType(@interface, FactoryFromType(implementation));

        private IRegisteredType RegisterType(Type itemType, Func<ILifetime, object> factory)
            => new RegisteredType(itemType, f => _registeredTypes[itemType] = f, factory);

        /// <summary>
        /// Returns the object registered for the given type, if registered
        /// </summary>
        /// <param name="type">Type as registered with the container</param>
        /// <returns>Instance of the registered type, if registered; otherwise <see langword="null"/></returns>
        public object GetService(Type type)
        {
            Func<ILifetime, object> registeredType;

            if (!_registeredTypes.TryGetValue(type, out registeredType))
            {
                return null;
            }

            return registeredType(_lifetime);
        }

        /// <summary>
        /// Creates a new scope
        /// </summary>
        /// <returns>Scope object</returns>
        public IScope CreateScope() => new ScopeLifetime(_lifetime);

        /// <summary>
        /// Disposes any <see cref="IDisposable"/> objects owned by this container.
        /// </summary>
        public void Dispose() => _lifetime.Dispose();

        #region Container items

        /// <summary>
        /// Compiles a lambda that calls the given type's first constructor resolving arguments
        /// </summary>
        /// <param name="itemType">Target type</param>
        /// <returns>Factory for <paramref name="itemType"/></returns>
        private static Func<ILifetime, object> FactoryFromType(Type itemType)
        {
            // Get first constructor for the type
            var constructors = itemType.GetConstructors();
            if (constructors.Length == 0)
            {
                // If no public constructor found, search for an internal constructor
                constructors = itemType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
            }

            var constructor = constructors.First();

            // Compile constructor call as a lambda expression
            var arg = Expression.Parameter(typeof(ILifetime));
            return (Func<ILifetime, object>)Expression.Lambda(
                Expression.New(constructor, constructor.GetParameters().Select(
                    param =>
                    {
                        var resolve = new Func<ILifetime, object>(
                            lifetime => lifetime.GetService(param.ParameterType));
                        return Expression.Convert(
                            Expression.Call(Expression.Constant(resolve.Target), resolve.Method, arg),
                            param.ParameterType);
                    })),
                arg).Compile();
        }

        /// <summary>
        /// RegisteredType is supposed to be a short lived object tying an item to its container
        /// and allowing users to mark it as a singleton or per-scope item
        /// </summary>
        class RegisteredType : IRegisteredType
        {
            private readonly Type _itemType;
            private readonly Action<Func<ILifetime, object>> _registerFactory;
            private readonly Func<ILifetime, object> _factory;

            /// <summary>
            /// Default ctor
            /// </summary>
            /// <param name="itemType">Target type</param>
            /// <param name="registerFactory">Factory for registration</param>
            /// <param name="factory">Factory</param>
            public RegisteredType(Type itemType, Action<Func<ILifetime, object>> registerFactory, Func<ILifetime, object> factory)
            {
                _itemType = itemType;
                _registerFactory = registerFactory;
                _factory = factory;

                registerFactory(_factory);
            }

            public void AsSingleton()
                => _registerFactory(lifetime => lifetime.GetServiceAsSingleton(_itemType, _factory));

            public void PerScope()
                => _registerFactory(lifetime => lifetime.GetServicePerScope(_itemType, _factory));
        }

        #endregion

        #region Private classes for lifetime management

        /// <summary>
        /// ObjectCache provides common caching logic for lifetimes
        /// </summary>
        abstract class ObjectCache : IDisposable
        {
            // Instance cache
            private readonly ConcurrentDictionary<Type, object> _instanceCache = new ConcurrentDictionary<Type, object>();

            // Get from cache or create and cache object
            protected object GetCached(Type type, Func<ILifetime, object> factory, ILifetime lifetime)
                => _instanceCache.GetOrAdd(type, _ => factory(lifetime));

            public void Dispose()
            {
                foreach (var obj in _instanceCache.Values)
                    (obj as IDisposable)?.Dispose();
            }
        }

        /// <summary>
        /// Container lifetime management
        /// </summary>
        class ContainerLifetime : ObjectCache, ILifetime
        {
            // Retrieves the factory functino from the given type, provided by owning container
            public Func<Type, Func<ILifetime, object>> GetFactory { get; private set; }

            public ContainerLifetime(Func<Type, Func<ILifetime, object>> getFactory) => GetFactory = getFactory;

            public object GetService(Type type) => GetFactory(type)(this);

            // Singletons get cached per container
            public object GetServiceAsSingleton(Type type, Func<ILifetime, object> factory)
                => GetCached(type, factory, this);

            // At container level, per-scope items are equivalent to singletons
            public object GetServicePerScope(Type type, Func<ILifetime, object> factory)
                => GetServiceAsSingleton(type, factory);
        }

        /// <summary>
        /// Per-scope lifetime management
        /// </summary>
        class ScopeLifetime : ObjectCache, ILifetime
        {
            // Singletons come from parent container's lifetime
            private readonly ContainerLifetime _parentLifetime;

            /// <summary>
            /// Default ctor
            /// </summary>
            /// <param name="parentContainer">Parent <see cref="ContainerLifetime"/></param>
            public ScopeLifetime(ContainerLifetime parentContainer) => _parentLifetime = parentContainer;

            public object GetService(Type type) => _parentLifetime.GetFactory(type)(this);

            // Singleton resolution is delegated to parent lifetime
            public object GetServiceAsSingleton(Type type, Func<ILifetime, object> factory)
                => _parentLifetime.GetServiceAsSingleton(type, factory);

            // Per-scope objects get cached
            public object GetServicePerScope(Type type, Func<ILifetime, object> factory)
                => GetCached(type, factory, this);
        }
        #endregion

    }
}
