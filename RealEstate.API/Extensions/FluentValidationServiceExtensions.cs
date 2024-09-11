using FluentValidation;
using RealEstate.Application.Validators;

namespace RealEstate.API.Extensions
{
    public static class FluentValidationServiceExtensions
    {
        public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreatePropertyValidator>();
            services.AddValidatorsFromAssemblyContaining<AddPropertyImageValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateOwnerValidator>();
            services.AddValidatorsFromAssemblyContaining<ChangePriceValidator>();
            services.AddValidatorsFromAssemblyContaining<CreatePropertyTraceValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateOwnerValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdatePropertyValidator>();

            return services;
        }
    }
}
