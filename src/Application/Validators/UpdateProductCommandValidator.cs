using Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            // Validate the Name (Required and Max Length)
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(50).WithMessage("Product name cannot exceed 50 characters.");

            // Validate ProduceDate (Required)
            RuleFor(x => x.ProduceDate)
                .NotEmpty().WithMessage("Produce date is required.");

            // Validate ManufactureEmail (Valid email format)
            RuleFor(x => x.ManufactureEmail)
                .NotEmpty().WithMessage("Manufacture email is required.")
                .EmailAddress().WithMessage("Manufacture email must be a valid email address.");

            // Validate ManufacturePhone (Starts with "09" and 11 characters long)
            RuleFor(x => x.ManufacturePhone)
                .Matches(@"^09\d{9}$").WithMessage("Manufacture phone must start with '09' and be 11 characters long.");

            // Validate IsAvailable (Required)
            RuleFor(x => x.IsAvailable)
                .NotNull().WithMessage("IsAvailable flag is required.");
        }
    }
}
