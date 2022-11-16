using MediatR;
using System.Reflection;

namespace RabbitMQ.Events
{
    public class IntegrationEventCollection : Dictionary<string, Type>
    {
        public IntegrationEventCollection()
        {
        }

        public IntegrationEventCollection(params Type[] handlerAssemblyTypes)
        {
            foreach (var type in handlerAssemblyTypes)
            {
                var eventTypes = Assembly.GetAssembly(type)?
                    .GetTypes()
                    .Where(_ => !_.IsAbstract && !_.IsInterface
                        && _.GetInterface(typeof(INotificationHandler<>).Name)?
                        .GetGenericArguments()?
                        .Any(generic => typeof(IntegrationEvent).IsAssignableFrom(generic)) == true
                    )
                    .Select(_ => _.GetInterface(typeof(INotificationHandler<>).Name)?
                            .GetGenericArguments()
                            .Single(generic => typeof(IntegrationEvent).IsAssignableFrom(generic))
                    );

                if (eventTypes != null && eventTypes.Any())
                {
                    foreach (var eventType in eventTypes)
                    {
                        TryAdd(eventType!.Name!, eventType);
                    }
                }
            }
        }
    }
}