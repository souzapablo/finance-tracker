using FinanceTracker.Api.Common.Dispatcher;

namespace FinanceTracker.Api.Extensions;

public static class DispatcherExtensions
{
    public static IServiceCollection RegisterDispatchers(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        builder.Services.AddScoped<IQueryDispatcher, QueryDispatcher>();
        RegisterCommands(builder);
        RegisterQueryHandlers(builder);
        return builder.Services;
    }

    private static void RegisterQueryHandlers(WebApplicationBuilder builder)
    {
        foreach (var type in AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => p.IsClass && !p.IsAbstract && p.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>))))
        {
            foreach (var interfaceType in type.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)))
            {
                builder.Services.AddScoped(interfaceType, type);
            }
        }
    }

    private static void RegisterCommands(WebApplicationBuilder builder)
    {
        foreach (var type in AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => p.IsClass && !p.IsAbstract && p.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>))))
        {
            foreach (var interfaceType in type.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>)))
            {
                builder.Services.AddScoped(interfaceType, type);
            }
        }
    }
}
