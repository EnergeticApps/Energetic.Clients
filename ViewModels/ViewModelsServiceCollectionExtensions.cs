using Energetic.Clients.ViewModels;
using Energetic.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ViewModelsServiceCollectionExtensions
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services, params Type[] assemblyMarkers)
        {
            var assemblies = assemblyMarkers.GetContainingAssemblies().ToArray();
            return services.AddViewModels(assemblies);
        }

        public static IServiceCollection AddViewModels(this IServiceCollection services, params Assembly[] assemblies)
        {
            if (assemblies.IsNullOrEmpty())
                throw new EnumerableArgumentEmptyException(nameof(assemblies));

            return services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo<IViewModel>())
            .AsMatchingInterface()
            .WithTransientLifetime());
        }
    }
}