using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Tyrion.Handlers;

namespace Tyrion.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTyrion(this IServiceCollection services, Type assemblyTypeOrigin)
        {
            services.AddTransient<ITyrion, Tyrion>();

            var assignedTypes = assemblyTypeOrigin.Assembly
                .GetTypes()
                .Where(x => x.GetInterfaces()
                .Any(y => y.IsGenericType && (
                    y.Name.Equals(typeof(IRequestHandler<,>).Name) || 
                    y.Name.Equals(typeof(IRequestHandler<>).Name))
                )).ToList();

            assignedTypes
            .ForEach(assignedType => assignedType.GetInterfaces()
                .Where(i => i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) ||
                            i.GetGenericTypeDefinition() == typeof(IRequestHandler<>)
                ).ToList()
            .ForEach(item => services.AddTransient(item, assignedType)));

            return services;
        }
    }
}
