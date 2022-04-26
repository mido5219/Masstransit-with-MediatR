using MassTransit;
using SharedModels;

namespace consumerAPI.Consumers
{
    public class ProductEventConsumer : IConsumer<ProductCreated>
    {
        public Task Consume(ConsumeContext<ProductCreated> context)
        {
            Console.WriteLine("EVENT FIRED");
            return Task.CompletedTask;
        }
    }
}
