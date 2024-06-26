using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using HOP.Bussiness.Constants;
using JewelrySalesSysmte_NoName_BE;
using JewelrySalesSystem_NoName_BE;
using JSS_BusinessObjects.SignalR;
using JSS_BusinessObjects.ZaloPay.Config;
using System.Text.Json.Serialization;


try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Logging.ClearProviders();

   var currentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;

    FirebaseApp.Create(new AppOptions()
    {
       
        Credential = GoogleCredential.FromFile($"{currentDirectory}\\jssimage-253a4-firebase-adminsdk-1ppe4-784c0284ad.json")
    });

    // Add services to the container.
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: CorsConstant.PolicyName,
           policy =>
           {
               policy.WithOrigins("https://localhost:7016") 
                     .WithOrigins("https://localhost:44328")
                     .AllowAnyHeader()
                     .AllowAnyMethod()
                     .AllowCredentials(); 
           });
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
    builder.Services.AddSignalRServices();
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

    //payment
    builder.Services.Configure<ZaloPayConfig>(
        builder.Configuration.GetSection(ZaloPayConfig.ConfigName));

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.UseMiddleware<AuthorizationHandlingMiddleware>();

    app.UseCors(CorsConstant.PolicyName);
    app.UseHttpsRedirection();

    app.UseRouting();
    app.UseCors("CorsPolicy");
    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapHub<SignalRServer>("/signalrServer");
    });

    app.Run();
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Exception: {ex.Message}\nStack Trace: {ex.StackTrace}");
    throw;
}