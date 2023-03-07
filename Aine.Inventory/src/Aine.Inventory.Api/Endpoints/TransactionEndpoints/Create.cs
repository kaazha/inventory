using System;
using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.Core.TransactionAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using FastEndpoints;

namespace Aine.Inventory.Api.Endpoints.TransactionEndpoints;

public class Create : Endpoint<CreateTransactionRequest, IEnumerable<ProductTransaction>>
{
  private readonly ITransactionService _service;

  public Create(ITransactionService service)
  {
    _service = service;
  }

  public override void Configure()
  {
    Post("/transactions");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CreateTransactionRequest request, CancellationToken cancellationToken)
  {
    request.UserName = User.UserName();
    var result = await _service.CreateTransaction(request, cancellationToken);
    switch (result.Status)
    {
      case ResultStatus.Ok:
        await SendAsync(result.Value, StatusCodes.Status201Created, cancellation: cancellationToken);
        break;
      case ResultStatus.Error:
        var msg = result.Errors != null ? string.Join(Environment.NewLine, result.Errors) : "An error has occurred!";
        await SendStringAsync(msg, StatusCodes.Status400BadRequest);
        break;
      case ResultStatus.Invalid:
        var msgs = result.ValidationErrors != null ? string.Join(Environment.NewLine, result.ValidationErrors.Select(v=>v.ErrorMessage)) : "Invalid data!";
        await SendStringAsync(msgs, StatusCodes.Status400BadRequest);
        break;
      default:
        await SendErrorsAsync();
        break;
    }
  }
}
