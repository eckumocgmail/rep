using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

internal class Utils
{
    internal static Dictionary<string, Dictionary<string, string>> ForAllPropertiesInType(Type type)
    {
        return new Dictionary<string, Dictionary<string, string>>((type.GetProperties().Select(p => new KeyValuePair<string, Dictionary<string, string>>(
                p.Name, type.GetPropertyAttributes(p.Name)))));
    }

    internal static Dictionary<string, Dictionary<string, string>> ForAllMethodsInType(Type type)
    {
        return new Dictionary<string, Dictionary<string, string>>((type.GetMethods().Select(p => new KeyValuePair<string, Dictionary<string, string>>(
                p.Name, type.GetMethodAttributes(p.Name)))));
    }

    internal static Dictionary<string, string> ForType(Type type)
    {
        return TypeAttributesExtensions.GetTypeAttributes(type);
    }

    internal static string LabelFor(Type type, string name)
    {
        return type.GetPropertyLabel(name);
    }

    internal static string DescriptionFor(Type type, string name)
    {
        return type.GetPropertyDescription(name);
    }

    internal static bool IsUniq(Type type, string name)
    {
        var attrs = type.GetPropertyAttributes(name);
        return attrs.Keys.Contains(nameof(UniqValueAttribute)) || attrs.ContainsKey(nameof(UniqueConstraint)) || attrs.ContainsKey(nameof(UniqueConstraintAttribute));
    }

    internal static string GetInputType(Type type, string name)
    {
        return type.GetPropertyInputType(name);
    }

    static internal Dictionary<string, string> ForProperty(Type type, string name)
    {
        return type.GetPropertyAttributes(name);
    }
}