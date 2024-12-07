using Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;


namespace Application.Validators
{
    public class CreateProductCommandValidator: AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            // Validate ManufactureEmail (Valid email format)
            RuleFor(x => x.ManufactureEmail)
                .NotEmpty().WithMessage("Manufacture email is required.")
                .EmailAddress().WithMessage("Manufacture email must be a valid email address.");

            RuleFor(x => x.ManufacturePhone)
                .NotEmpty().WithMessage("Manufacture phone is required.")
                .Matches(@"^09\d{9}$").WithMessage("Manufacture phone must start with '09' and be exactly 11 characters long.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(50).WithMessage("Product name cannot exceed 50 characters.");

            RuleFor(x => x.ProduceDate)
                .NotEmpty().WithMessage("Produce date is required.");

            RuleFor(x => x.IsAvailable)
                .NotNull().WithMessage("IsAvailable flag is required.");
        }

    }
}
