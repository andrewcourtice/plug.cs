using System;

namespace Plug.Exceptions
{
    public class DuplicateRegistrationException : Exception
    {
        public Type RegistrationType { get; }

        public DuplicateRegistrationException(Type registrationType)
            : base($"An instance of { registrationType.Name } has already been registered")
        {
            RegistrationType = registrationType;
        }
    }
}
