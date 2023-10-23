using System;

namespace Tyrion
{
    public static class TypeExtensions
    {
        public static bool Is(this Type type, Type typeCompare) => type.IsGenericType && (type.Name.Equals(typeCompare.Name) || type.GetGenericTypeDefinition() == typeCompare);
    }
}
