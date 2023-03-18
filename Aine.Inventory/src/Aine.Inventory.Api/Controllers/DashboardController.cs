using System;
using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aine.Inventory.Api.Controllers;

[Route("api/[controller]")]
public class SummaryController : BaseApiController
{
  private readonly ISummaryRepository _summaryRepository;

  public SummaryController(ISummaryRepository summaryRepository)
  {
    _summaryRepository = summaryRepository;
  }

  [HttpGet("")]
  public async Task<CategoryAndProductSummary> GetCategoryAndProductSummary(CancellationToken cancellationToken)
  {
    return await _summaryRepository.GetCategoryAndProductSummary(cancellationToken);
  }
}