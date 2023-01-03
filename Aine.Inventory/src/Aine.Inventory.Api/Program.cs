using System.Text.Json.Serialization;
using Aine.Inventory.Api;
using Aine.Inventory.Infrastructure;
using Aine.Inventory.Infrastructure.Data;
using Aine.Inventory.SharedKernel;
using Ardalis.ListStartupServices;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FastEndpoints;
using FastEndpoints.ApiExplorer;
using FastEndpoints.Swagger.Swashbuckle;
using MediatR;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

const string DESCRIPTION = "Aine Inventory Api 1.0";

var builder = WebApplication.CreateBuilder(args);
{

  builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

  builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

  builder.Services.Configure<CookiePolicyOptions>(options =>
  {
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
  });

  
  builder.Services.AddMediatR(typeof(Program));
  
  string? connectionString = builder.Configuration.GetConnectionString("SqliteConnection");

  builder.Services.AddDbContext<AppDbContext>(
    builder =>
      builder.UseSqlite(connectionString!)
        .LogTo(Console.WriteLine)
        .EnableDetailedErrors()
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
  );  
  //builder.Services.AddControllersWithViews().AddNewtonsoftJson();
  //builder.Services.AddRazorPages();
  builder.Services.AddFastEndpoints();
  builder.Services.AddFastEndpointsApiExplorer();
  builder.Services.AddSwaggerGen(c =>
  {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = DESCRIPTION, Version = "v1" });
    c.EnableAnnotations();
    c.OperationFilter<FastEndpointsOperationFilter>();
  });

  // add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
  builder.Services.Configure<ServiceConfig>(config =>
  {
    config.Services = new List<ServiceDescriptor>(builder.Services);

    // optional - default path to view services is /listallservices - recommended to choose your own path
    config.Path = "/listservices";
  });


  builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
  {
    containerBuilder.RegisterAllTypesFromAssembly(typeof(SharedKernelMarker).Assembly, builder.Environment.EnvironmentName);
    containerBuilder.RegisterAllTypesFromAssembly(typeof(InfrastructureMarker).Assembly, builder.Environment.EnvironmentName);
    //containerBuilder.RegisterModule(new DefaultCoreModule());
    //containerBuilder.RegisterModule(new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development"));
  });

  builder.Services.Configure<JsonOptions>(options =>
  {
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    //options.SerializerOptions.IgnoreReadOnlyProperties = true;
  });
}

var app = builder.Build();
{
  //app.UsePathBase("/aine-inventory-api");

  app.UseExceptionHandler(a => a.Run(ErrorHandler.HandleException));
  app.UseHsts();

  //if (app.Environment.IsDevelopment())
  //{
  //  app.UseDeveloperExceptionPage();
  //  app.UseShowAllServicesMiddleware();
  //}
  //else
  //{
  //  app.UseExceptionHandler("/Home/Error");
  //  app.UseHsts();
  //}
  app.UseRouting();
  try {
    app.UseFastEndpoints();
  } catch(Exception ex)
  {
    Console.WriteLine(ex.Message);
  }

  app.UseHttpsRedirection();
  app.UseStaticFiles();
  app.UseCookiePolicy();

  // Enable middleware to serve generated Swagger as a JSON endpoint.
  app.UseSwagger();

  // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
  app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", DESCRIPTION));

  //app.MapDefaultControllerRoute();
  //app.MapRazorPages();

}

app.Run();

