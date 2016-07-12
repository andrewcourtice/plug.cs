using Plug.Factories;
using System;

namespace Plug.Extensions
{
    public static class RegistrationExtensions
    {
        public static void RegisterAsDependency(this Type registrationType, Type instanceType, Container container, IFactory factory)
        {
            container.Register(registrationType, instanceType, factory);
        }

        public static void RegisterAsInstance(this Type instanceType, Type registrationType, Container container, IFactory factory)
        {
            container.Register(registrationType, instanceType, factory);
        }
    }
}
