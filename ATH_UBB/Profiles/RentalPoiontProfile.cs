using AutoMapper;
using ATH_UBB.Model;
using ATH_UBB.Models;

namespace ATH_UBB.Profiles
{
    public class RentalPoiontProfile : Profile
    {
        public RentalPoiontProfile()
        {
            CreateMap<RentalPoint, RentalPointViewModel>().ForMember(dest => dest.City, opt => opt.MapFrom(x=> x.City))
                .ForMember(dest => dest.Adres, opt => opt.MapFrom(x => x.Adres));
            CreateMap<RentalPointViewModel, RentalPoint>().ForMember(dest => dest.Vehicles, opt => opt.Ignore());
        }
    }
}
