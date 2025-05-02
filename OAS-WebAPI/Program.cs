
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OAS_ClassLib;
using OAS_ClassLib.Interfaces;
using OAS_ClassLib.Repositories;
using System.Text;

namespace OAS_WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //builder.Services.AddScoped<AuctionServices>();
            //builder.Services.AddScoped<UserServices>();
            //builder.Services.AddScoped<OAS_ClassLib.Repositories.ProductServices>();
            builder.Services.AddScoped<IAuctionService, AuctionServices>();
            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<IReviewInsertService, ReviewServices>();
            builder.Services.AddScoped<IReviewRetrieveService, ReviewServices>();
            builder.Services.AddScoped<IReviewDeleteService, ReviewServices>();
            builder.Services.AddScoped<IReviewStatisticsService, ReviewServices>();
            builder.Services.AddScoped<IReviewQueryService, ReviewServices>();
            builder.Services.AddScoped<IReviewAnalysisService, ReviewServices>();
            builder.Services.AddScoped<ITransactionService, TransactionServices>();
            builder.Services.AddScoped<IBidService, BidServices>();
            builder.Services.AddScoped<IBidStatisticsService, BidServices>();
            builder.Services.AddScoped<IBidGroupingService, BidServices>();
            builder.Services.AddScoped<IBidQueryService, BidServices>();
            builder.Services.AddScoped<IProductCrudService, ProductServices>();
            builder.Services.AddScoped<IProductImageService, ProductServices>();
            builder.Services.AddScoped<IProductStatisticsService, ProductServices>();
            builder.Services.AddScoped<IProductQueryService, ProductServices>();
            builder.Services.AddDbContext<AppDbContext>();

            //Retriving the secret key from the configuration
            var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");

            //SETIING UP THE AUTHENTICATION 

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

                //CONIFIGURING JWT HANDLING 
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });


            //Integrates JWT authentication into Swagger documentation.

            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "bearer",
                            Name = "Authorization",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            });
            



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                });
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowLocalhost");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}

