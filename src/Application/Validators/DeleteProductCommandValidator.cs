using Application.Commands;
using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            RuleFor(x => x.ManufactureEmail)
                .NotEmpty().WithMessage("Manufacture email is required.");

            RuleFor(x => x.ProduceDate)
                .NotEmpty().WithMessage("Produce date is required.");

            RuleFor(x => x)
                .MustAsync(ValidateProductAvailabilityAsync)
                .WithMessage("Product must be available (IsAvailable = true) to delete.");
        }

        private async Task<bool> ValidateProductAvailabilityAsync(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByEmailAndDateAsync(command.ManufactureEmail, command.ProduceDate);
            return product?.IsAvailable == true; // Checks if product is available
        }
    }
}
