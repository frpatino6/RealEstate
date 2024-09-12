using MediatR;
using RealEstate.Application.Commands;
using RealEstate.Application.Interfaces;
using RealEstate.Application.Services;
using RealEstate.Domain.Interfaces;
using RealEstate.Domain.Repositories;
using RealEstate.Infrastructure.Repositories;

namespace RealEstate.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IPropertyTraceRepository, PropertyTraceRepository>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IPropertyImageRepository, PropertyImageRepository>();

            services.AddMediatR(typeof(UpdateOwnerCommandHandler).Assembly);
            services.AddScoped<IMapperPropertyService, MapperPropertyService>();
            services.AddScoped<IPropertyFilter, PropertyFilter>();
            services.AddScoped<IMapperOwnerService, MapperOwnerService>();


            return services;
        }
    }
}
