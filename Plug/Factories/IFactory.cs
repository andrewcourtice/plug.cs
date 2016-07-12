
namespace Plug.Factories
{
    public interface IFactory
    {
        /// <summary>
        /// Resolve an instance of a registration
        /// </summary>
        /// <typeparam name="T">The dependency type of the registration (the interface type)</typeparam>
        /// <param name="registration">The registration to resolve</param>
        /// <returns>An instance of the registration</returns>
        void Resolve(Registration registration);
    }
}
