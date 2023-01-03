select ProductID, [ProductNumber], Name, [ProductSubcategoryID], [ProductModelID], [Color], [Size],
		[SizeUnitMeasureCode], [Weight] , [WeightUnitMeasureCode], [Style], [ReorderPoint], 
		[StandardCost] ,[ListPrice], 1
	FROM [AdventureWorks].[Production].[Product]

  INSERT INTO products ( id,product_number,name, sub_category_id, model_id, color,
						 size, size_measurement, [weight], weight_measurement, style, reorder_point, 
						standard_cost, list_price, is_active)
  VALUES
  
  select distinct [ProductSubcategoryID] from [AdventureWorks].[Production].[Product]
  select distinct [ProductModelID] from [AdventureWorks].[Production].[Product]

  select concat('(', ProductID, ',''' + [ProductNumber] + ''',', '''' + Replace(Name, '''', '''''') + ''',', 
            IIF([ProductSubcategoryID] is null, 'null,', cast([ProductSubcategoryID] as varchar(10)) + ','), 
            IIF([ProductModelID] is null, 'null,', cast([ProductModelID] as varchar(10)) + ','),              
            IIF([Color] is null, 'null,', '''' + [color] + ''','), 
            IIF([Size] is null, 'null,', '''' + cast(Rtrim([size]) as varchar(10))  + ''','),
		IIF([SizeUnitMeasureCode] is null, 'null,', '''' + Rtrim([SizeUnitMeasureCode]) + ''','), 
        IIF([Weight] is null, 'null,', cast([Weight] as varchar(10))  + ','), 
        IIF([WeightUnitMeasureCode] is null, 'null,', '''' + Rtrim([WeightUnitMeasureCode]) + ''','),
        IIF([Style] is null, 'null,', '''' + Rtrim([Style]) + ''','), 
        IIF([ReorderPoint] is null, 'null,', cast([ReorderPoint] as varchar(10))  + ','),  
		IIF([StandardCost] is null, 'null,', cast([StandardCost] as varchar(10))  + ','),
        IIF([ListPrice] is null, 'null,', cast([ListPrice] as varchar(10))  + ','),        
        1, '), ')
	FROM [AdventureWorks].[Production].[Product]
    
