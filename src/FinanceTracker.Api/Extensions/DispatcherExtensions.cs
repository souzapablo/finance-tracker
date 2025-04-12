using FinanceTracker.Api.Common.Dispatcher;

namespace FinanceTracker.Api.Extensions;

public static class DispatcherExtensions
{
    public static void RegisterDispatchers(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
        builder.Services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
        RegisterCommands(builder);
        RegisterQueryHandlers(builder);
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
                builder.Services.AddSingleton(interfaceType, type);
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
                builder.Services.AddSingleton(interfaceType, type);
            }
        }
    }
}
