using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using HOP.Bussiness.Constants;
using JewelrySalesSysmte_NoName_BE;
using JewelrySalesSystem_NoName_BE;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;


try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Logging.ClearProviders();

   var currentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;

    FirebaseApp.Create(new AppOptions()
    {
       
        //Credential = GoogleCredential.FromFile($"{currentDirectory}\\jssimage-253a4-firebase-adminsdk-1ppe4-784c0284ad.json")
        Credential = GoogleCredential.FromFile($"D:\\PrjGit\\Summer2024_PRN231_NET1716_GroupNoName\\JewelrySalesSystem_NoName_BE\\JewelrySalesSystem_NoName_BE\\jssimage-253a4-firebase-adminsdk-1ppe4-784c0284ad.json")
    }); 

    // Add services to the container.
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: CorsConstant.PolicyName,
            policy => { policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod(); });
    });

    builder.Services.AddControllers().AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        x.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });
    var configuration = builder.Configuration;

    builder.Services.AddDatabase();
    builder.Services.AddUnitOfWork();
    builder.Services.AddServices();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddConfigSwagger();
    builder.Services.AddSingletonJson();
    builder.Services.AddJwtValidation(configuration);
    // Add JWT Authentication Middleware - This code will intercept HTTP request and validate the JWT.
    //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    //    opt => {
    //        opt.TokenValidationParameters = new TokenValidationParameters
    //        {
    //            ValidateIssuerSigningKey = true,
    //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
    //            .GetBytes("tokengroupnonametokengroupnonametokengroupnonametokengroupnonametokengroupnoname")),
    //            ValidateIssuer = false,
    //            ValidateAudience = false
    //        };
    //    }
    //  );

    //builder.Services.AddAuthorization(options =>
    //{
    //    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    //    options.AddPolicy("StaffPolicy", policy => policy.RequireRole("Staff"));
    //    options.AddPolicy("ManagerPolicy", policy => policy.RequireRole("Manager"));
    //});

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.UseMiddleware<AuthorizationHandlingMiddleware>();

    app.UseCors(CorsConstant.PolicyName);
    //app.UseHttpsRedirection();
    app.UseHttpsRedirection();
    app.Use(async (context, next) =>
    {
        await next();
        if (context.Response.StatusCode == (int)System.Net.HttpStatusCode.Unauthorized)
        {
            await context.Response.WriteAsync("Token Validation Has Failed. Request Access Denied");
        }
    });

    //need to sperate middleware for each api
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    throw new Exception("Stop program because of exception");
}