using AutoMapper;
using ATH_UBB.Model;
using ATH_UBB.Models;

namespace ATH_UBB.Profiles
{
    public class RentalPoiontProfile : Profile
    {
        public RentalPoiontProfile()
        {
            CreateMap<RentalPoint, RentalPoionViewModel>().ForMember(dest => dest.Name, opt => opt.MapFrom(x=> x.City + " " + x.Adres));
            CreateMap<RentalPoionViewModel, RentalPoint>().ForMember(dest => dest.Vehicles, opt => opt.Ignore());
        }
    }
}
