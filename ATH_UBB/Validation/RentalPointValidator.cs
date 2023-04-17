using ATH_UBB.Models;
using FluentValidation;

namespace ATH_UBB.Validation
{
    public class RentalPointValidator : AbstractValidator<RentalPointViewModel>
    {
        public RentalPointValidator()
        {
            RuleFor(rental => rental.City).NotEmpty().MinimumLength(5).WithMessage("Miasto musi mieć co najmniej 5 znaków");
            RuleFor(rental => rental.Adres).NotEmpty().MinimumLength(5).WithMessage("Adres musi mieć co najmniej 5 znaków");
        }
    }
}
