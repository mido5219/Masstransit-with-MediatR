using Application.Contracts;
using Application.Products.Commands.CreateProduct;
using Application.Products.Queries.GetProducts;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SharedModels;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ConsumerEndpointsSettings _consumerEndpointsConfig;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IPublishEndpoint _publishEndpoint;

        public ProductsController(ISendEndpointProvider sendEndpointProvider,IPublishEndpoint publishEndpoint, IMediator mediator, IOptions<ConsumerEndpointsSettings> config)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
            _consumerEndpointsConfig = config.Value;

        }
        [HttpPost("[action]")]
        public async Task<ActionResult<CreateProductResponse>> CreateProduct(CreateProductCommand model)
        {
            var response = await _mediator.Send(model);
            return Ok(response);
        }
        [HttpGet("[action]")]
        public async Task<ActionResult<GetProductsResponse>> GetProducts()
        {
            return Ok(await _mediator.Send(new GetProductsQuery()));
        }
        [HttpGet("[action]")]
        public async Task<ActionResult> TestSendProductPublished()
        {
            var serviceAddress = new Uri(_consumerEndpointsConfig.ActionAssignedEmailServiceAddress);
            await _publishEndpoint.Publish<ProductCreated>(new ProductCreated (1,"PUBLISH Created PRODUCT"));
            return Ok();
        }
        [HttpGet("[action]")]
        public async Task<ActionResult> TestSendProductCreated()
        {
            var serviceAddress = new Uri(_consumerEndpointsConfig.ProductCreatedServiceAddress);
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(serviceAddress);
            await endpoint.Send(new ProductCreated(1, "SEND Created PRODUCT"));
            return Ok();
        }
        [HttpGet("[action]")]
        public async Task<ActionResult> TestSendProductDeleted()
        {
            var serviceAddress = new Uri(_consumerEndpointsConfig.ProductDeletedServiceAddress);
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(serviceAddress);
            await endpoint.Send(new ProductDeleted(1));
            //var serviceAddress = new Uri(_consumerEndpointsConfig.ActionAssignedEmailServiceAddress);
            //var endpoint = await _sendEndpointProvider.GetSendEndpoint(serviceAddress);
            //await endpoint.Send(new EmailConfirmation("CONTENT CONTENT"));
            return Ok();
        }



    }
}
