using System;
using Ardalis.Specification;

namespace Aine.Inventory.Core.ProductAggregate.Specifications;

public class FindProductByProductNumberSpecification : Specification<Product, ProductRecord>
{
  public FindProductByProductNumberSpecification(string productNumber)
  {
    Query
      .Select(p => new ProductRecord {  Id = p.Id, Name = p.Name})
      .Where(p => p.ProductNumber == productNumber);
  }
}

public class ProductRecord
{
  public int? Id { get; set; }
  public string? Name { get; set; }
}
