using System.ComponentModel.DataAnnotations.Schema;

namespace Aine.Inventory.SharedKernel;

// This can be modified to EntityBase<TId> to support multiple key types (e.g. Guid)
public abstract class EntityBase
{
  private readonly List<DomainEventBase> _domainEvents = new ();

  [NotMapped]
  [IgnoreMember]
  [System.Text.Json.Serialization.JsonIgnore]
  public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

  protected internal void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);
  internal void ClearDomainEvents() => _domainEvents.Clear();
}

public abstract class EntityBase<TId> : EntityBase where TId : struct
{
  public TId Id { get; set; }
}
