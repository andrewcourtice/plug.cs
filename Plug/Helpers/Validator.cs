using System;

namespace Plug.Helpers
{
    internal static class Validator
    {
        internal static void Required(object value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }
    }
}
