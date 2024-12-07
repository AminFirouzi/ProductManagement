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
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IValidator<DeleteProductCommand> _validator;

        public DeleteProductCommandHandler(IProductRepository productRepository, IValidator<DeleteProductCommand> validator)
        {
            _productRepository = productRepository;
            _validator = validator;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            // Validate the request using the validator
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

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
