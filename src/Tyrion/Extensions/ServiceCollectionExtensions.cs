using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Tyrion.Handlers;

namespace Tyrion.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddTyrion(this IServiceCollection services, Type type)
        {
            services.AddSingleton<ITyrion, Tyrion>();

            var types = type.Assembly
                .GetTypes()
                .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && (
                    y.Name.Equals(typeof(IRequestHandler<,>).Name) ||
                    y.Name.Equals(typeof(IRequestHandler<>).Name))
                ))
                .ToList();

            types
                .ForEach(assignedType => assignedType.GetInterfaces()
                    .Where(i => i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) ||
                                i.GetGenericTypeDefinition() == typeof(IRequestHandler<>))
                    .ToList()
                    .ForEach(item => services.AddScoped(item, assignedType)));
        }
    }
}