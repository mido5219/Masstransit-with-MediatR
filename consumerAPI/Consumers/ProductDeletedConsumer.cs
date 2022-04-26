using MassTransit;
using SharedModels;

namespace consumerAPI.Consumers
{
    public class ProductDeletedConsumer : IConsumer<ProductDeleted>
    {
        public Task Consume(ConsumeContext<ProductDeleted> context)
        {
            Console.WriteLine($"Product Deleted Id: {context.Message.Id.ToString()} SEND Deleted Product");
            return Task.CompletedTask;
        }
    }
}
