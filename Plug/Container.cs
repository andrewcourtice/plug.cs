using Plug.Exceptions;
using Plug.Factories;
using Plug.Helpers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Plug
{
    public class Container : MarshalByRefObject, IDisposable
    {
        /// <summary>
        /// Private field to handle disposal
        /// </summary>
        private bool disposedValue = false;

        /// <summary>
        /// Private field for storing registrations in the current container
        /// </summary>
        private readonly ConcurrentDictionary<Type, Registration> registrations;

        /// <summary>
        /// The configuration of this container
        /// </summary>
        public ContainerConfiguration Configuration { get; }

        /// <summary>
        /// The current list of registrations
        /// </summary>
        public ICollection<Registration> Registrations
        {
            get { return registrations.Values; }
        }

        /// <summary>
        /// Create a new container to store registrations
        /// </summary>
        /// <param name="configuration">The initial configuration for the container</param>
        public Container(ContainerConfiguration configuration)
        {
            Validator.Required(configuration, nameof(configuration));

            Configuration = configuration;

            registrations = new ConcurrentDictionary<Type, Registration>(configuration.ConcurrencyLevel, 0);
        }

        /// <summary>
        /// Create a new container to store registrations
        /// </summary>
        public Container() : this(new ContainerConfiguration()) { }

        public static Container NewContainer()
        {
            return new Container();
        }

        /// <summary>
        /// Create a new container to store registrations
        /// </summary>
        /// <param name="configuration">The initial configuration for the container</param>
        /// <returns></returns>
        public static Container NewContainer(ContainerConfiguration configuration)
        {
            return new Container(configuration);
        }

        /// <summary>
        /// Create a new container to store registrations
        /// </summary>
        /// <param name="scope">The scope in which to create this container</param>
        /// <returns></returns>
        public static Container NewContainer(Scope scope)
        {
            return scope.CreateObject<Container>();
        }

        /// <summary>
        /// Create a new container to store registrations
        /// </summary>
        /// <param name="scope">The scope in which to create this container</param>
        /// <param name="configuration">The initial configuration for the container</param>
        /// <returns></returns>
        public static Container NewContainer(Scope scope, ContainerConfiguration configuration)
        {
            return scope.CreateObject<Container>(configuration);
        }

        /// <summary>
        /// Register a new dependency
        /// </summary>
        /// <param name="registration">The registration object to register</param>
        private void AddRegistration(Registration registration)
        {
            var isRegistered = registrations.TryAdd(registration.RegistrationType, registration);

            if (!isRegistered)
            {
                throw new DuplicateRegistrationException(registration.RegistrationType);
            }
        }

        /// <summary>
        /// Register a new dependency
        /// </summary>
        /// <param name="registrationType">The dependency type of this registration (the interface type)</param>
        /// <param name="instanceType">The instance type of this registration</param>
        /// <param name="factory">The factory responsible for resolving this registration</param>
        public Registration Register(Type registrationType, Type instanceType, IFactory factory)
        {
            var registration = new Registration(registrationType, instanceType, factory ?? Configuration.DefaultFactory, this);
            AddRegistration(registration);
            return registration;
        }

        /// <summary>
        /// Register a new dependency
        /// </summary>
        /// <param name="registrationType">The dependency type of this registration (the interface type)</param>
        /// <param name="instanceType">The instance type of this registration</param>
        public Registration Register(Type registrationType, Type instanceType)
        {
            return Register(registrationType, instanceType, null);
        }

        /// <summary>
        /// Register a new dependency
        /// </summary>
        /// <typeparam name="T">The dependency type of this registration (the interface type)</typeparam>
        /// <param name="instanceType">The instance type of this registration</param>
        /// <param name="factory">The factory responsible for resolving this registration</param>
        public Registration Register<T>(Type instanceType, IFactory factory)
        {
            return Register(typeof(T), instanceType, factory);
        }

        /// <summary>
        /// Register a new dependency
        /// </summary>
        /// <typeparam name="T">The dependency type of this registration (the interface type)</typeparam>
        /// <param name="instanceType">The instance type of this registration</param>
        public Registration Register<T>(Type instanceType)
        {
            return Register(typeof(T), instanceType, null);
        }

        /// <summary>
        /// Register a new dependency
        /// </summary>
        /// <typeparam name="TD">The dependency type of this registration (the interface type)</typeparam>
        /// <typeparam name="TI">The instance type of this registration</typeparam>
        /// <param name="factory">The factory responsible for resolving this registration</param>
        public Registration Register<TD, TI>(IFactory factory) where TI : class
        {
            return Register<TD>(typeof(TI), factory);
        }

        /// <summary>
        /// Register a new dependency
        /// </summary>
        /// <typeparam name="TD">The dependency type of this registration (the interface type)</typeparam>
        /// <typeparam name="TI">The instance type of this registration</typeparam>
        public Registration Register<TD, TI>() where TI : class
        {
            return Register<TD>(typeof(TI), null);
        }

        /// <summary>
        /// Remove a registration from the container
        /// </summary>
        /// <param name="registrationType">The dependency type of this registration (the interface type)</param>
        /// <returns></returns>
        public Registration Remove(Type registrationType)
        {
            Registration registration;
            registrations.TryRemove(registrationType, out registration);
            return registration;
        }

        /// <summary>
        /// Remove a registration from the container
        /// </summary>
        /// <typeparam name="T">The dependency type of this registration (the interface type)</typeparam>
        /// <returns></returns>
        public Registration Remove<T>()
        {
            return Remove(typeof(T));
        }

        /// <summary>
        /// Get the registration for the specified type
        /// </summary>
        /// <param name="registrationType">The dependency type of the registration (the interface type)</param>
        /// <returns></returns>
        public Registration GetRegistration(Type registrationType)
        {
            Registration registration;

            var isRegistered = registrations.TryGetValue(registrationType, out registration);

            if (!isRegistered || registration == null)
            {
                throw new NotRegisteredException(registrationType);
            }

            return registration;
        }

        /// <summary>
        /// Get the registration for the specified type
        /// </summary>
        /// <typeparam name="T">The dependency type of the registration (the interface type)</typeparam>
        /// <returns></returns>
        public Registration GetRegistration<T>()
        {
            return GetRegistration(typeof(T));
        }

        /// <summary>
        /// Resolve an instance of the specified type
        /// </summary>
        /// <param name="registrationType">The dependency type of the registration (the interface type)</param>
        /// <returns>The instance of the registration generated by it's assigned factory</returns>
        public object Resolve(Type registrationType)
        {
            var registration = GetRegistration(registrationType);
            return registration.Resolve();
        }

        /// <summary>
        /// Resolve an instance of the specified type
        /// </summary>
        /// <typeparam name="T">The dependency type of the registration (the interface type)</typeparam>
        /// <returns>The instance of the registration generated by it's assigned factory</returns>
        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        /// <summary>
        /// Validate the current container to ensure all registrations are valid and there are no cyclic dependencies
        /// </summary>
        public void Validate()
        {
            Validator.ValidateContainer(this);
        }

        /// <summary>
        /// Clear all existing registrations from the container
        /// </summary>
        /// <param name="cleanup">Whether the garbage collector should be forced to reclaim used memory from registrations</param>
        public void Flush(bool cleanup)
        {
            registrations.Clear();

            if (cleanup)
            {
                GC.Collect(0, GCCollectionMode.Optimized);
            }
        }

        /// <summary>
        /// Standard disposal implementation
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    registrations.Clear();
                }

                disposedValue = true;
            }
        }

        /// <summary>
        /// Standard disposal implementation
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
