using System;
using System.Reflection;

namespace CopaFilmes.Tests.Common.Util;

internal static class ReflectionHelper
{
	internal static PropertyInfo GetPropertyInfo(this object @object, string propertyName)
	{
		if (@object is null)
			throw new ArgumentNullException(nameof(@object));

		var objType = @object.GetType();
		PropertyInfo propInfo;
		do
		{
			propInfo = objType.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			objType = objType.BaseType;
		}
		while (propInfo == null && objType != null);
		if (propInfo == null)
		{
			string message = $@"Couldn't find property {propertyName} in type {objType?.FullName ?? string.Empty}";
			throw new ArgumentOutOfRangeException(nameof(propertyName), message);
		}

		return propInfo;
	}

	internal static object GetPropertyValue(this object @object, string propertyName)
		=> GetPropertyValue<object>(@object, propertyName);
	internal static TType GetPropertyValue<TType>(this object @object, string propertyName)
		=> (TType)GetPropertyInfo(@object, propertyName).GetValue(@object);

	internal static void SetPropertyValue(this object @object, string propertyName, object value)
		=> GetPropertyInfo(@object, propertyName).SetValue(@object, value);

	internal static void SetPrivatePropertyValue(this object @object, string propertyName, object value)
		=> GetPropertyInfo(@object, $"<{propertyName}>k__BackingField").SetValue(@object, value);
}
