using EventsApi.Brokers.Infrastructure;
using EventsApi.Brokers.Mymessagebroker;
using Microsoft.Extensions.DependencyInjection;

namespace EventsApi.Brokers.Mymessagebroker;

public class MymessagebrokerConsumerService
    : KafkaConsumerService<MymessagebrokerMessageHandlersController>
{
    public MymessagebrokerConsumerService(
        IServiceScopeFactory serviceScopeFactory,
        KafkaOptions kafkaOptions
    )
        : base(serviceScopeFactory, kafkaOptions) { }
}
