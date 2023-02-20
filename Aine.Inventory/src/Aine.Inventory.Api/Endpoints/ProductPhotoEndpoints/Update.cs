//using Aine.Inventory.Core.ProductPhotoAggregate;
//using Aine.Inventory.SharedKernel.Interfaces;
//using FastEndpoints;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;

//namespace Aine.Inventory.Api.Endpoints.ProductPhotoEndpoints;

//public class UpdateProductPhoto : EndpointWithoutRequest
//{
//  public UpdateProductPhoto()
//  {
//  }

//  public override void Configure()
//  {
//    //Post("/products/{ProductId}/photo");
//    //Put("/products/{ProductId}/photo");
//    AllowAnonymous();
//    AllowFileUploads(dontAutoBindFormData: true); //turns off buffering
//  }

//  public override async Task HandleAsync(CancellationToken ct)
//  {
//    await foreach (var section in FormFileSectionsAsync(ct))
//    {
//      if (section is not null)
//      {
//        using (var fs = System.IO.File.Create(section.FileName))
//        {
//          await section.Section.Body.CopyToAsync(fs, 1024 * 64, ct);
//        }
//      }
//    }

//    await SendOkAsync("upload complete!");
//  }

//  /*
//  public override async Task HandleAsync(UpdateProductPhotoRequest request, CancellationToken cancellationToken)
//  {
//    await SendAsync($"Photo Received! ProductId={request.ProductId}, Length={request.Photo?.Length}");
//  }

//  public async Task HandleAsync2(CancellationToken ct)
//  {
//    if (Files.Count > 0)
//    {
//      var file = Files[0];

//      await SendStreamAsync(
//          stream: file.OpenReadStream(),
//          fileName: "test.png",
//          fileLengthBytes: file.Length,
//          contentType: "image/png");

//      return;
//    }
//    await SendNoContentAsync();
//  }
//  */
//}

//public class UpdateProductPhotoRequest
//{
//  public int ProductId { get; set; }
//  public IFormFile Photo { get; set; } = default!;
//}

