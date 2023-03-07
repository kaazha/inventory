using System;
using System.Collections.Generic;
using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.Core.TransactionAggregate;
using Aine.Inventory.SharedKernel;
using Aine.Inventory.SharedKernel.Guard;
using Aine.Inventory.SharedKernel.Interfaces;
using Aine.Inventory.SharedKernel.Security;
using Ardalis.Result;
using MediatR;

namespace Aine.Inventory.Core.Services;

[Inject]
public class TransactionService : ITransactionService
{
  private readonly IRepository<ProductTransaction> _repository;

  public TransactionService(IRepository<ProductTransaction> repository)
  {
    _repository = repository;
  }

  public async Task<Result<IEnumerable<ProductTransaction>>> CreateTransaction(CreateTransactionRequest transaction, CancellationToken cancellationToken)
  {
    ArgumentNullException.ThrowIfNull(transaction, nameof(transaction));
    var errors = new List<string>();
    Guard.Against.NullOrEmpty(transaction.TransactionType, () => errors.Add("Transaction Type is required."));
    Guard.Against.NotOneOf(transaction.TransactionType, ProductTransaction.TransactionTypes, (value) => errors.Add($"Invalid Transaction Type '{value}'."));
    Guard.Against.NullOrEmpty(transaction.Items, () => errors.Add("At least one Transaction item is required."));
    //if (transaction.Items.All(i => i.ProductId <= 0 && i.Quantity <= 0))
    //  errors.Add("Invalid transaction items. ProductId and Quantity must be specified!");
    var invalidItem = transaction.Items.FirstOrDefault(i => i.Quantity <= 0);
    if (invalidItem is { }) errors.Add($"Invalid quantity {invalidItem.Quantity}.");

    if (errors.Any())
    {
      var validationErrors = errors.Select(error => new ValidationError { ErrorMessage = error }).ToList();
      return Result<IEnumerable<ProductTransaction>>.Invalid(validationErrors);
    }

    var transactionEntries = transaction.Items
         .Where(item => item.ProductId > 0)
        .Select(item => ProductTransaction.Create(
      id: 0,
      item.ProductId,
      transaction.TransactionType,
      transaction.TransactionDate,
      transaction.ReferenceNumber,
      item.Quantity,
      item.TotalCost,
      transaction.Notes,
      transaction.UserName
      ))
      .ToArray();

    var transactions = await _repository.AddRangeAsync(transactionEntries, cancellationToken);
    return transactions != null && transactions.Any(t => t.Id > 0)
          ? Result<IEnumerable<ProductTransaction>>.Success(transactions)
          : Result<IEnumerable<ProductTransaction>>.Error();
  }
}

