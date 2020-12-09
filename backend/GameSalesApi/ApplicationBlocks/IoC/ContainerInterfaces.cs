using System;

namespace ApplicationBlocks.IoC
{
    /// <summary>
    /// Represents a scope in which per-scope objects are instantiated a single time
    /// </summary>
    public interface IScope : IDisposable, IServiceProvider
    { }
    
    /// <summary>
    /// IRegisteredType is return by Container.Register and allows further configuration for the registration
    /// </summary>
    public interface IRegisteredType
    {
        /// <summary>
        /// Make registered type a singleton
        /// </summary>
        void AsSingleton();
    
        /// <summary>
        /// Make registered type a per-scope type (single instance within a Scope)
        /// </summary>
        void PerScope();
    }

    /// <summary>
    /// ILifetime management adds resolution strategies to an IScope
    /// </summary>
    interface ILifetime : IScope
    {
        /// <summary>
        /// Get servises that was added like <see cref="IRegisteredType.AsSingleton"/> by <paramref name="type"/>
        /// </summary>
        /// <param name="type">Target type</param>
        /// <param name="factory">Factory</param>
        /// <returns>Target singleton object</returns>
        object GetServiceAsSingleton(Type type, Func<ILifetime, object> factory);

        /// <summary>
        /// Get servises that was added like <see cref="IRegisteredType.PerScope"/> by <paramref name="type"/>
        /// </summary>
        /// <param name="type">Target type</param>
        /// <param name="factory">Factory</param>
        /// <returns>Target object for scope</returns>
        object GetServicePerScope(Type type, Func<ILifetime, object> factory);
    }
}
