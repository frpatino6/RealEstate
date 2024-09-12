using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.API.Extensions;
using RealEstate.API.Middleware;
using RealEstate.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Real Estate API",
        Version = "v1"
    });
    c.EnableAnnotations(); // Habilita las anotaciones para que funcione SwaggerOperation
});

builder.Services.AddDbContext<RealEstateDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(typeof(RealEstate.Application.AssemblyReference).Assembly);

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();



builder.Services.AddProjectServices();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddFluentValidationServices();


builder.Services.AddControllers(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});



var app = builder.Build();
app.UseMiddleware<ErrorHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Real Estate API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();