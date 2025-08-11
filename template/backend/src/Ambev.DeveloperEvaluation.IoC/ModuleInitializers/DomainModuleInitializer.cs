using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class DomainModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ISaleDomainService, SaleDomainService>();
        builder.Services.AddSingleton<ISaleItemDomainService, SaleItemDomainService>();
        builder.Services.AddSingleton<IEventPublisherService, ConsoleEventPublisherService>();
    }
}