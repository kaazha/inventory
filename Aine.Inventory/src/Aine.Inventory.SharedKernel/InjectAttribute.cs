using Aine.Inventory.SharedKernel.Interfaces;

namespace Aine.Inventory.SharedKernel;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class InjectAttribute: Attribute
{
  public InjectAttribute(
    InstanceScope scope = InstanceScope.InstancePerLifetimeScope,
    InjectType injectType = InjectType.ImplementedInterfaces,
    Type registerAs = default!,
    string environment = default!)
  {
    if (injectType.HasFlag(InjectType.Other) && registerAs is null)
      throw new ArgumentNullException(nameof(registerAs));
    InjectType = injectType;
    Scope = scope;
    Environment = environment;
    RegisterAs = registerAs;
  }

  public string Environment { get; set; }
  public InstanceScope Scope { get; set; } = InstanceScope.InstancePerLifetimeScope;
  public InjectType InjectType { get; set; } = InjectType.ImplementedInterfaces;
  public Type? RegisterAs { get; set; }
  public Type? TypeFinder { get; set; }
}

[Flags]
public enum InjectType
{
  ImplementedInterfaces = 1,
  Self = 2,
  Other = 4
}

public enum InstanceScope
{ 
  InstancePerLifetimeScope,
  Transient,
  Singleton
}

public interface ITypeEnumerator
{
  IEnumerable<Type> GetTypes();
}