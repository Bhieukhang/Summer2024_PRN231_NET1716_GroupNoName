using HOP.Bussiness.Constants;
using JewelrySalesSysmte_NoName_BE;
using JewelrySalesSystem_NoName_BE;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Text.Json.Serialization;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Logging.ClearProviders();
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
    builder.Services.AddDatabase();
    builder.Services.AddUnitOfWork();
    builder.Services.AddServices();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddConfigSwagger();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMiddleware<ExceptionHandlingMiddleware>();

    app.UseCors(CorsConstant.PolicyName);
    //app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    throw new Exception("Stop program because of exception");
}