using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByEmailAndDateAsync(request.ManufactureEmail, request.ProduceDate);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            await _productRepository.DeleteProductAsync(product);
            return Unit.Value;
        }

        Task IRequestHandler<DeleteProductCommand>.Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
