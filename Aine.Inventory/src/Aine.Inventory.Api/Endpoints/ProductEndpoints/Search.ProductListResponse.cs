﻿using Aine.Inventory.Core.CategoryAggregate;
using Aine.Inventory.Core.Interfaces;

namespace Aine.Inventory.Api.Endpoints.ProductEndpoints;

public class ProductSearchResponse
{
  public IEnumerable<ProductDto> Products { get; set; } = new List<ProductDto>();
}

public class ProductDto : IProduct
{
  public int Id { get; set;  }
  public string ProductNumber { get; set;  } = default!;
  public string Name { get; set; } = default!;
  public string? Description { get; set; }
  //public string? Description { get => _description ?? Model?.Description; set => _description = value;  }
  public int? SubCategoryId { get; set;  }
  public int? ModelId { get; set;  }
  public string? Color { get; set;  }
  public string? Size { get; set;  }
  public string? SizeUnit { get; set;  }
  public double? Weight { get; set;  }
  public string? WeightUnit { get; set;  }
  public string? Style { get; set;  }
  public int? ReorderPoint { get; set;  }
  public double? StandardCost { get; set;  }
  public double? ListPrice { get; set;  }
  public bool IsActive { get; set;  }
  public string? ModelName { get; set; }
  public string? CategoryName { get; set; }
  public int? CategoryId { get; set; }
  public string? SubCategoryName { get; set; }
  //public ProductSubCategory? SubCategory { get; set; }
  //public ProductCategory? Category => this.SubCategory?.Category;
}
