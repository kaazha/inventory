
using Aine.Inventory.Core.CategoryAggregate;

namespace Aine.Inventory.Api.Endpoints.ModelEndpoints;

public record ModelRecord(
  int Id,
  string Name,
  string? Description
);

