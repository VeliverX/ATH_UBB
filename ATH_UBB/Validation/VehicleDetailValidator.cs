using FluentValidation;
using ATH_UBB.Models;
namespace ATH_UBB.Validation
{
    public class VehicleDetailValidator : AbstractValidator<VehicleDetailViewModel>
    {
        public VehicleDetailValidator()
        {
            RuleFor(vehicle => vehicle.model).NotEmpty().WithMessage("Model musi zostac okreslony");
            RuleFor(vehicle => vehicle.brand).NotEmpty().WithMessage("Marka musi zostać okreslona");
            RuleFor(vehicle => vehicle.description).MinimumLength(10).WithMessage("Opis musi posiadać co najmniej 10 znaków");
            RuleFor(vehicle => vehicle.price).GreaterThan(0).WithMessage("Cena musi byc większa od zera");
        }
    }
}
