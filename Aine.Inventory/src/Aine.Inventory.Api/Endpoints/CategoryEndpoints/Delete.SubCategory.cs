using System.Threading;
using Aine.Inventory.Core.CategoryAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace Aine.Inventory.Api.Endpoints.CategoryEndpoints;

public class DeleteSubCategory : EndpointWithoutRequest
{ 
  private readonly IRepository<ProductSubCategory> _repository;

  public DeleteSubCategory(IRepository<ProductSubCategory> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Delete("/subcategories/{subcategoryId}");
    AllowAnonymous();
    DontCatchExceptions();
    Summary(s =>
    {
      s.Summary = "Deletes a Product Sub-Category";
      s.Params["subcategoryId"] = "SubCategory Id";
    });
  }

  public override async Task HandleAsync(
    CancellationToken cancellationToken)
  {
    var id = Route<int>("subcategoryId", true);
    var deleted = await _repository.DeleteByIdAsync(id, cancellationToken);
    if (deleted <= 0) await SendNotFoundAsync(cancellationToken);
    await SendOkAsync(cancellationToken);
  }
}
