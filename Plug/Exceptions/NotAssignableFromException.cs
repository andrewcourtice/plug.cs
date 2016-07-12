using System;

namespace Plug.Exceptions
{
    public class NotAssignableFromException : Exception
    {
        public Type BaseType { get; }
        public Type AssigneeType { get; }

        public NotAssignableFromException(Type baseType, Type assigneeType)
            : base($"{ baseType.Name } is not assignable from { assigneeType.Name }")
        {
            BaseType = baseType;
            AssigneeType = assigneeType;
        }
    }
}
