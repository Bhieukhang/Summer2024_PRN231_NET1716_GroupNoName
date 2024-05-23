using JSS_DataAccessObjects;
using JSS_Services.Implement;
using JSS_Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using JSS_Repositories;
using System.Reflection;
using JSS_BusinessObjects.Helper;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_BE
{
    public static class DependencyServices
    {
        public static IConfiguration Configuration { get; }


        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork<JewelrySalesSystemContext>, UnitOfWork<JewelrySalesSystemContext>>();
            return services;
        }

        public static IServiceCollection AddSingletonJson(this IServiceCollection services)
        {
            services.AddSingleton<JsonSerializerSettings>(JsonSerializationHelper.GetNewtonsoftJsonSerializerSettings());
            return services;
        }
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<JewelrySalesSystemContext>(options =>
                options.UseSqlServer(GetConnectionString()));
            return services;
        }

        private static string GetConnectionString()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", true, true)
                        .Build();
            var strConn = config["ConnectionStrings:DefaultConnection"];

            return strConn;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Ví dụ với AddScoped:
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IStallService, StallService>();
            services.AddScoped<IWarrantyService, WarrantyService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductMaterialService,ProductMaterialService>();
            services.AddScoped<IMaterialService, MaterialService>();
            services.AddScoped<ICategoryService, CategoryService>();
            return services;
        }

        //public static IServiceCollection AddJwtValidation(this IServiceCollection services)
        //{
        //    services.AddAuthentication(options =>
        //    {
        //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    }).AddJwtBearer(options =>
        //    {
        //        options.TokenValidationParameters = new TokenValidationParameters()
        //        {
        //            ValidIssuer = "JewerlySalesSystem",
        //            ValidateIssuer = true,
        //            ValidateAudience = false,
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey =
        //                new SymmetricSecurityKey(
        //                    Encoding.UTF8.GetBytes("HOP"))
        //        };
        //    });
        //    return services;
        //}

        public static IServiceCollection AddConfigSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = "Jewelry Sales System Documentation",
                    Description = "Jewelry Sales System"
                });

                // Lấy đường dẫn tới file XML của tài liệu
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                options.IncludeXmlComments(xmlPath);

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
                options.MapType<TimeOnly>(() => new OpenApiSchema
                {
                    Type = "string",
                    Format = "time",
                    Example = OpenApiAnyFactory.CreateFromJson("\"13:45:42.0000000\"")
                });
            });
            return services;
        }

        //public static IServiceCollection AddAuthentication(this IServiceCollection services)
        //{
        //    _ = services.AddAuthentication(options =>
        //    {
        //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    })
        //            .AddJwtBearer(options =>
        //            {
        //                options.RequireHttpsMetadata = false;
        //                options.SaveToken = true;
        //                options.TokenValidationParameters = new TokenValidationParameters
        //                {
        //                    ValidateIssuer = false,
        //                    ValidateAudience = false,
        //                    ValidateIssuerSigningKey = true,
        //                    IssuerSigningKey =
        //                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AppSettings:SecretKey"]))
        //                };
        //            });
        //    return services;
        //}
    }
}

