using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CustomExtensions.Editor
{
    public static class ReflectionExtensionsEditor
    {
        public static string GetFieldName<TClass, TField>(this TClass _,
            BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance)
            where TClass : class =>
            typeof(TClass)
                .GetFields(bindingFlags)
                .First(fieldInfo => fieldInfo.FieldType == typeof(TField))
                .Name;

        public static string GetListFieldName<TClass, TElement>(this TClass _,
            BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance)
            where TClass : class =>
            typeof(TClass)
                .GetFields(bindingFlags)
                .First(fieldInfo => fieldInfo.FieldType == typeof(List<TElement>))
                .Name;
    }
}