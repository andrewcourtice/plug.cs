using System;

namespace Plug.Exceptions
{
    public class NotRegisteredException : Exception
    {
        public Type RegistrationType { get; }

        public NotRegisteredException(Type registrationType)
            : base($"An instance of { registrationType.Name } has not been registered")
        {
            RegistrationType = registrationType;
        }
    }
}
