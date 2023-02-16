using System;
using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.Core.ProductAggregate;
using Aine.Inventory.SharedKernel.Interfaces;

namespace Aine.Inventory.Core.Services;

public class ProductService : IProductService
{
  private readonly IRepository<Product> _repository;

  public ProductService(IRepository<Product> repository)
  {
    _repository = repository;
  }

  public Task<IProduct> SaveAsync(IProduct product)
  {
    throw new NotImplementedException();
  }
}

