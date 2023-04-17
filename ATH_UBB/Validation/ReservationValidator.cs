using ATH_UBB.Model;
using FluentValidation;

namespace ATH_UBB.Validation
{


    public class ReservationValidator : AbstractValidator<Reservation>
    {
        public ReservationValidator()
        {
            RuleFor(reservation => reservation.StartDay)
                .NotEmpty().WithMessage("Data startowa jest wymagana.")
                .Must(date => date.Date >= DateTime.Now.Date).WithMessage("Data startowa musi być dzisiejszą datą lub późniejsza.");

            RuleFor(reservation => reservation.EndDay)
            .NotEmpty().WithMessage("Data końcowa jest wymagana.")
            .Must((reservation, dateEnd, context) => dateEnd > reservation.StartDay)
            .WithMessage("Data końcowa musi być późniejsza niż data startowa.");
        }
    }
}
