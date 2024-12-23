using EventsWorker.Brokers.Infrastructure;
using EventsWorker.Brokers.Mymessagebroker;
using Microsoft.Extensions.DependencyInjection;

namespace EventsWorker.Brokers.Mymessagebroker;

public class MymessagebrokerConsumerService
    : KafkaConsumerService<MymessagebrokerMessageHandlersController>
{
    public MymessagebrokerConsumerService(
        IServiceScopeFactory serviceScopeFactory,
        KafkaOptions kafkaOptions
    )
        : base(serviceScopeFactory, kafkaOptions) { }
}
