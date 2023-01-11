using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Aine.Inventory.Api;

public static class AppBuilderExtensions
{
   /*
  public static void AddJwtAuthentication(this WebApplicationBuilder builder)
  {
    builder.Services.AddAuthentication(options =>
    {
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
      .AddJwtBearer(o =>
    {
      o.TokenValidationParameters = GetTokenValidationParameters(builder);
    });
  }

  public static TokenValidationParameters GetTokenValidationParameters(this WebApplicationBuilder builder)
  {
    return new TokenValidationParameters
    {
      //ValidIssuer = builder.Configuration["Jwt:Issuer"],
      //ValidAudience = builder.Configuration["Jwt:Audience"],
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
      ValidateIssuer = false,
      ValidateAudience = false,
      ValidateLifetime = true,
      ValidateIssuerSigningKey = true
    };
  }

 
  public static TokenValidationParameters GetTokenValidationParameters(this WebApplicationBuilder builder, TokenValidationParameters p)
  {
    p.ValidIssuer = builder.Configuration["Jwt:Issuer"];
    p.ValidAudience = builder.Configuration["Jwt:Audience"];
    p.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]));
    p.ValidateIssuer = true;
    p.ValidateAudience = true;
    p.ValidateLifetime = false;
    p.ValidateIssuerSigningKey = true;
    return p;
  }
  */

  public static void AddApiSecurityDefinition(this SwaggerGenOptions option)
  {
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
      In = ParameterLocation.Header,
      Description = "Please enter a valid token",
      Name = "Authorization",
      Type = SecuritySchemeType.Http,
      BearerFormat = "JWT",
      Scheme = "Bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
  }
}
