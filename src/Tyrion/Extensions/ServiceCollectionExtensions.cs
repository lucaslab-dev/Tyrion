using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;

namespace Tyrion
{
    public static class ServiceCollectionExtensions
    {
        private static bool IsRequestHandlers(Type type) => type.Is(typeof(IRequestHandler<,>)) || type.Is(typeof(IRequestHandler<>)) || type.Is(typeof(INotificationHandler<>));

        public static void AddTyrion(this IServiceCollection services, Type type)
        {
            services.AddScoped<ITyrion, Tyrion>();
            services.AddRequestHandlers(type);
            services.AddValidators(type);
        }

        private static void AddRequestHandlers(this IServiceCollection services, Type type) => type.Assembly
            .GetTypes()
            .Where(type => type.GetInterfaces().Any(IsRequestHandlers)).ToList()
            .ForEach(type => type.GetInterfaces().Where(IsRequestHandlers).ToList().ForEach(@interface => services.TryAddScoped(@interface, type)));

        private static void AddValidators(this IServiceCollection services, Type type) => type.Assembly
            .GetTypes()
            .Where(type => type.BaseType.Is(typeof(Validator<>))).ToList()
            .ForEach(type => services.TryAddScoped(type.BaseType, type));
    }
}