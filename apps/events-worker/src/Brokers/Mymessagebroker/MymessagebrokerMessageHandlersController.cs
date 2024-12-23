using System.Threading.Tasks;
using EventsWorker.Brokers.Infrastructure;

namespace EventsWorker.Brokers.Mymessagebroker;

public class MymessagebrokerMessageHandlersController
{
    [Topic("event.created")]
    public Task HandleEventCreated(string message)
    {
        //set your message handling logic here

        return Task.CompletedTask;
    }
}
