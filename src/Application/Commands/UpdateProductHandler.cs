using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByEmailAndDateAsync(request.ManufactureEmail, request.ProduceDate);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            product.Name = request.Name;
            product.ManufacturePhone = request.ManufacturePhone;
            product.IsAvailable = request.IsAvailable;

            await _productRepository.UpdateProductAsync(product);
            return Unit.Value;
        }

        Task IRequestHandler<UpdateProductCommand>.Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
