using System.Threading;
using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.Core.ProductAggregate;
using Aine.Inventory.Core.ProductPriceAggregate;
using Aine.Inventory.SharedKernel;
using Aine.Inventory.SharedKernel.Interfaces;
using Aine.Inventory.SharedKernel.Security;
using Ardalis.Result;
using MediatR;

namespace Aine.Inventory.Core.Services;

[Inject]
public class PriceService : IPriceService
{
  private readonly IRepository<ProductPrice> _repository;

  public PriceService(IRepository<ProductPrice> repository)
  {
    _repository = repository;
  }

  public async Task<Result<ProductPrice>> CreatePriceAsync(IPriceChange request, CancellationToken cancellationToken)
  {
    var productPrices = await _repository.ListAsync(new ProductPriceSpecification(request.ProductId), cancellationToken);

    if (CreatesOverlap(productPrices, request))
      return Result<ProductPrice>.Error("Price effective period creates an overlap!");

    var currentPrice = productPrices.FirstOrDefault();
    //if(currentPrice != null && request.EffectiveDate < currentPrice.EndDate)
    //  return Result<ProductPrice>.Error("Price effective date must be after the creates an overlap!");

    var priceChange = currentPrice != null ? Math.Round(request.ListPrice - currentPrice.ListPrice, 2) : (double?)null;
    var priceInfo = ProductPrice.Create(
      id: 0,
      request.ProductId,
      request.EffectiveDate,
      request.EndDate,
      request.ListPrice,
      priceChange,
      request.ChangedBy,
      DateTime.UtcNow,
      request.Notes
    );
    
    if (currentPrice is not null)
    {
      currentPrice!.SetEndDate(request.EffectiveDate.AddDays(-1));
      await _repository.UpdateAsync(currentPrice, cancellationToken);
    }

    var createdItem = await _repository.AddAsync(priceInfo, cancellationToken);
    return Result<ProductPrice>.Success(createdItem);
  }

  public async Task<Result<ProductPrice>> UpdatePriceAsync(IProductPrice request, CancellationToken cancellationToken)
  {
    var productPrices = await _repository.ListAsync(new ProductPriceSpecification(request.ProductId), cancellationToken);

    if (CreatesOverlap(productPrices, request))
      return Result<ProductPrice>.Error("Price effective period creates an overlap!");

    var prevPrice = productPrices.FirstOrDefault(p => p.EndDate < request.EffectiveDate);
    var priceChange = prevPrice != null ? Math.Round(request.ListPrice - prevPrice.ListPrice, 2) : (double?)null;
    var priceInfo = ProductPrice.Create(
    id: request.Id,
      request.ProductId,
      request.EffectiveDate,
      request.EndDate,
      request.ListPrice,
      priceChange,
      request.ChangedBy,
      DateTime.UtcNow,
      request.Notes
    );

    await _repository.UpdateAsync(priceInfo, cancellationToken);
    return Result<ProductPrice>.Success(priceInfo);
  }

  private bool CreatesOverlap(ICollection<ProductPrice> productPrices, IPriceChange request)
  {
    if (!productPrices.Any()) return false;
    return productPrices.Any(p => Overlap(p.EffectiveDate, p.EndDate, request.EffectiveDate, request.EndDate));
  }

  public static bool Overlap(DateTime startDate1, DateTime? endDate1, DateTime startDate2, DateTime? endDate2)
  {
    return startDate1 < endDate2 && startDate2 < endDate1;
  }
}