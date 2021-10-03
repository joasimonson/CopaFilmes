using System;
using System.Reflection;

namespace CopaFilmes.Tests.Common.Util
{
    internal static class ReflectionHelper
    {
        private static PropertyInfo GetPropertyInfo(Type type, string propertyName)
        {
            PropertyInfo propInfo;
            do
            {
                propInfo = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                type = type.BaseType;
            }
            while (propInfo == null && type != null);
            return propInfo;
        }

        public static object GetPropertyValue(this object obj, string propertyName)
        {
            return GetPropertyValue<object>(obj, propertyName);
        }
        public static TType GetPropertyValue<TType>(this object obj, string propertyName)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            var objType = obj.GetType();
            var propInfo = GetPropertyInfo(objType, propertyName);
            if (propInfo == null)
            {
                throw new ArgumentOutOfRangeException(nameof(propertyName), $@"Couldn't find property {propertyName} in type {objType.FullName}");
            }
            return (TType)propInfo.GetValue(obj, null);
        }

        public static void SetPropertyValue(this object obj, string propertyName, object val)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            var objType = obj.GetType();
            var propInfo = GetPropertyInfo(objType, propertyName);
            if (propInfo == null)
            {
                throw new ArgumentOutOfRangeException(nameof(propertyName), $@"Couldn't find property {propertyName} in type {objType.FullName}");
            }
            propInfo.SetValue(obj, val, null);
        }
    }
}
