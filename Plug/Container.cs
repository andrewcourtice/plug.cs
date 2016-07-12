using System;
using Plug.Factories;
using Plug.Exceptions;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Plug.Helpers;

namespace Plug
{
    public class Container : IDisposable
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

        /// <summary>
        /// Register a new dependency
        /// </summary>
        /// <param name="registration">The registration object to register</param>
        public void Register(Registration registration)
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
        public void Register(Type registrationType, Type instanceType, IFactory factory)
        {
            var registration = new Registration(registrationType, instanceType, factory ?? Configuration.DefaultFactory);
            Register(registration);
        }

        /// <summary>
        /// Register a new dependency
        /// </summary>
        /// <param name="registrationType">The dependency type of this registration (the interface type)</param>
        /// <param name="instanceType">The instance type of this registration</param>
        public void Register(Type registrationType, Type instanceType)
        {
            Register(registrationType, instanceType, null);
        }

        /// <summary>
        /// Register a new dependency
        /// </summary>
        /// <typeparam name="T">The dependency type of this registration (the interface type)</typeparam>
        /// <param name="instanceType">The instance type of this registration</param>
        /// <param name="factory">The factory responsible for resolving this registration</param>
        public void Register<T>(Type instanceType, IFactory factory)
        {
            Register(typeof(T), instanceType, factory);
        }

        /// <summary>
        /// Register a new dependency
        /// </summary>
        /// <typeparam name="T">The dependency type of this registration (the interface type)</typeparam>
        /// <param name="instanceType">The instance type of this registration</param>
        public void Register<T>(Type instanceType)
        {
            Register(typeof(T), instanceType, null);
        }

        /// <summary>
        /// Register a new dependency
        /// </summary>
        /// <typeparam name="TD">The dependency type of this registration (the interface type)</typeparam>
        /// <typeparam name="TI">The instance type of this registration</typeparam>
        /// <param name="factory">The factory responsible for resolving this registration</param>
        public void Register<TD, TI>(IFactory factory)
        {
            Register<TD>(typeof(TI), factory);
        }

        /// <summary>
        /// Register a new dependency
        /// </summary>
        /// <typeparam name="TD">The dependency type of this registration (the interface type)</typeparam>
        /// <typeparam name="TI">The instance type of this registration</typeparam>
        public void Register<TD, TI>()
        {
            Register<TD>(typeof(TI), null);
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
        private Registration getRegistration(Type registrationType)
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
        /// Resolve an instance of the specified type
        /// </summary>
        /// <param name="registrationType">The dependency type of the registration (the interface type)</param>
        /// <returns>The instance of the registration generated by it's assigned factory</returns>
        public object Resolve(Type registrationType)
        {
            var registration = getRegistration(registrationType);
            return registration.Resolve();
        }

        /// <summary>
        /// Resolve an instance of the specified type
        /// </summary>
        /// <typeparam name="T">The dependency type of the registration (the interface type)</typeparam>
        /// <returns>The instance of the registration generated by it's assigned factory</returns>
        public T Resolve<T>()
        {
            return (T) Resolve(typeof(T));
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
