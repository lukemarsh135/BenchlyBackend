using BenchlyBackend.Infrastructure.Attributes;
using System.Reflection;

namespace BenchlyBackend;

public static class Registration
{
    public static IServiceCollection AddServices (this IServiceCollection services, Assembly assembly)
    {
        var interfacesWithAttribute = assembly.GetTypes()
            .Where(t => t.IsInterface && t.GetCustomAttribute<RegistrationAttribute>() != null);

        foreach (var interfaceType in interfacesWithAttribute)
        {
            var attribute = interfaceType.GetCustomAttribute<RegistrationAttribute>()
                ?? throw new InvalidOperationException("");

            var implementations = assembly.GetTypes()
                .Where(t => interfaceType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                .ToList();

            if (implementations.Count == 0)
            {
                continue;
            }

            foreach (var impl in implementations)
            {
                switch (attribute.Lifetime)
                {
                    case ServiceLifetime.Singleton:
                        services.AddSingleton(interfaceType, impl);
                        break;

                    case ServiceLifetime.Scoped:
                        services.AddScoped(interfaceType, impl);
                        break;

                    case ServiceLifetime.Transient:
                        services.AddTransient(interfaceType, impl);
                        break;
                }
            }
        }

        return services;
    }
}
