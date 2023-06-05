using ATH_UBB.Model;
using ATH_UBB.Models;
using AutoMapper;

namespace ATH_UBB.Profiles
{
    public class ReservationsProfile : Profile
    {
        public ReservationsProfile()
        {
            CreateMap<Reservation, ReservationViewModel>();
            CreateMap<ReservationViewModel, Reservation>().ForMember(dest => dest.Vehicles, opt => opt.Ignore());
        }
    }
}
