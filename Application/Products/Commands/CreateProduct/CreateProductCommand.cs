using Domain.Entities;
using Infrastructure.Persistence;
using MassTransit;
using MediatR;
using SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Commands.CreateProduct
{
    public record CreateProductCommand(string Name, double Price):IRequest<CreateProductResponse>;
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand,CreateProductResponse>
    {
        private readonly ApplicationDbContext _context;

        public CreateProductCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return new CreateProductResponse(product);
        }
    }
}
