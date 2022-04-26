using Infrastructure.Persistence;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Queries.GetProducts
{
    public record GetProductsQuery():IRequest<GetProductsResponse>;
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery,GetProductsResponse>
    {
        public readonly ApplicationDbContext _context;
        public GetProductsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetProductsResponse> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Products.ToListAsync();
            return new GetProductsResponse(products);
        }
    }
}
