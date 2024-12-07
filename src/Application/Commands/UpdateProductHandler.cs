using Application.Interfaces;
using FluentValidation;
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
        private readonly IValidator<UpdateProductCommand> _validator;

        public UpdateProductCommandHandler(IProductRepository productRepository, IValidator<UpdateProductCommand> validator)
        {
            _productRepository = productRepository;
            _validator = validator;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            // Validate the request using the validator
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                // Handle validation failure (e.g., throw an exception or return error)
                throw new ValidationException(validationResult.Errors);
            }

            var product = await _productRepository.GetProductByEmailAndDateAsync(request.ManufactureEmail, request.ProduceDate);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            // Update product properties
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
