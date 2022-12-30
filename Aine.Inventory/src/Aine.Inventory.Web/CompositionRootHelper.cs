using System.Reflection;
using Aine.Inventory.SharedKernel;
using Autofac;
using Autofac.Builder;

namespace Aine.Inventory.Web;

public static class CompositionRootHelper
{
  /// <summary>
  /// Registers all types in a specified assembly marked with the InjectAttribute
  /// </summary>
  /// <exception cref="InvalidOperationException"></exception>
  public static void RegisterAllTypesFromAssembly(this ContainerBuilder builder, Assembly assembly, string environment)
  {
    foreach (var type in assembly.GetTypes())
    {
      var attribute = type.GetCustomAttribute<InjectAttribute>();
      if (attribute is null) continue;
      if (!string.IsNullOrEmpty(attribute.Environment) &&
          !string.Equals(attribute.Environment, environment, StringComparison.OrdinalIgnoreCase)) continue;

      IEnumerable<Type> types = new[] { type };
      if (attribute.TypeFinder is not null && type.ContainsGenericParameters)
      {
        var finder = (ITypeEnumerator)Activator.CreateInstance(attribute.TypeFinder!)!;
        types = finder.GetTypes().Select(t => type.MakeGenericType(t));
      }

      foreach (var targetType in types)
      {
        Console.WriteLine($"Registering {targetType.FullName}");
        switch (attribute!.Scope)
        {
          case InstanceScope.InstancePerLifetimeScope:
            builder.RegisterType(targetType).RegisterAs(attribute).InstancePerLifetimeScope();
            break;
          case InstanceScope.Singleton:
            builder.RegisterType(targetType).RegisterAs(attribute).SingleInstance();
            break;
          case InstanceScope.Transient:
            builder.RegisterType(targetType).RegisterAs(attribute).InstancePerDependency();
            break;
          default:
            throw new InvalidOperationException($"Invalid registration option: {attribute.Scope}");
        }
      }
    }
  }

  private static IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> RegisterAs(
    this IRegistrationBuilder<object, ConcreteReflectionActivatorData,
    SingleRegistrationStyle> builder,
    InjectAttribute attr)
  {
    if (attr.InjectType.HasFlag(InjectType.ImplementedInterfaces)) builder.AsImplementedInterfaces();
    if (attr.InjectType.HasFlag(InjectType.Self)) builder.AsSelf();
    if (attr.InjectType.HasFlag(InjectType.Other)) builder.As(attr.RegisterAs!);

    return builder;
  }
}
