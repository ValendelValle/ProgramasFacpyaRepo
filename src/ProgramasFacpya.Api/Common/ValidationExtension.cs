namespace ProgramasFacpya.Api.Common;
using FluentValidation;
public static class ValidationExtensions
{
    public static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<Program>();
        return services;
    }
}
