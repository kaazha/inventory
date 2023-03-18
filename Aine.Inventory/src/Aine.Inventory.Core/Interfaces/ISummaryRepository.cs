using System;
using Aine.Inventory.Core.Models;

namespace Aine.Inventory.Core.Interfaces;

public interface ISummaryRepository
{
  Task<CategoryAndProductSummary> GetCategoryAndProductSummary(CancellationToken cancellationToken = default);
}