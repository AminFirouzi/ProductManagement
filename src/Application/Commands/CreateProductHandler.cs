using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
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
        private readonly IValidator<CreateProductCommand> _validator;

        public CreateProductCommandHandler(IProductRepository productRepository, IValidator<CreateProductCommand> validator)
        {
            _productRepository = productRepository;
            _validator = validator;
        }

        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // Validate the request using the validator
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                // Handle validation failure (e.g., throw an exception or return error)
                throw new ValidationException(validationResult.Errors);
            }

            var product = new Product
            {
                Name = request.Name,
                ProduceDate = request.ProduceDate,
                ManufacturePhone = request.ManufacturePhone,
                ManufactureEmail = request.ManufactureEmail,
                IsAvailable = request.IsAvailable
            };

            await _productRepository.AddProductAsync(product);
            return Unit.Value;
        }

        Task IRequestHandler<CreateProductCommand>.Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
