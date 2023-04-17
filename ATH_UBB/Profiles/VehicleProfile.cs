using AutoMapper;
using ATH_UBB.Model;
using ATH_UBB.Models;

namespace ATH_UBB.Profiles
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<Vehicle, VehicleItemViewModel>().ForMember(dest => dest.model, opt => opt.MapFrom(x => x.Name));
            CreateMap<Vehicle, VehicleDetailViewModel>().ForMember(dest => dest.isAvailable, opt => opt.MapFrom(x => x.Reserv.IsReserved))
                .ForMember(dest => dest.localization, opt =>opt.MapFrom(x=> x.RentalPoint.City + " " + x.RentalPoint.Adres));
            CreateMap<VehicleDetailViewModel, Vehicle>().ForMember(dest => dest.RentalPoint, opt => opt.Ignore()).ForMember(dest => dest.RentalId, opt => opt.Ignore())
                .ForMember(dest => dest.Type,opt=>opt.Ignore()).ForMember(dest => dest.TypeId, opt => opt.Ignore()).ForMember(dest => dest.Reserv,opt=>opt.Ignore())
                .ForMember(dest => dest.ReservationId, opt => opt.Ignore());

        }
    }
}
