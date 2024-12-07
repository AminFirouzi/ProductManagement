using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

         Task IRequestHandler<CreateProductCommand>.Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                ProduceDate = request.ProduceDate,
                ManufacturePhone = request.ManufacturePhone,
                ManufactureEmail = request.ManufactureEmail,
                IsAvailable = request.IsAvailable
            };

            return _productRepository.AddProductAsync(product)
                .ContinueWith(_ => Unit.Value, cancellationToken);
        }
    }
}
