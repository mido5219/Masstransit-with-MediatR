using MassTransit;
using SharedModels;

namespace consumerAPI.Consumers
{
    public class ProductCreatedConsumer:IConsumer<ProductCreated>
    {
        public Task Consume(ConsumeContext<ProductCreated> context)
        {
            Console.WriteLine($"Product Created, Id: {context.Message.Id.ToString()}, Name: {context.Message.Name}" );
            return Task.CompletedTask;
        }
    }
}
