using AutoMapper;
using PgccApi.Entities;
using PgccApi.Models;
using PgccApi.Models.ViewModels;

namespace PgccApi.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Inbound
            CreateMap<EnquiryModel, Enquiry>()
                .ForMember(o => o.Id, opt => opt.Ignore())
                .ForMember(o => o.When, opt => opt.Ignore());

            // Outbound
            CreateMap<Rink, RinkViewModel>()
                .ForMember(o => o.Season, opt => opt.MapFrom(src => src.Season.Name))
                .ForMember(o => o.Competition, opt => opt.MapFrom(src => src.Competition.Name));
        }
    }
}
