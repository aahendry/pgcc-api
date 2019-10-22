using AutoMapper;
using PgccApi.Entities;
using PgccApi.Models;

namespace PgccApi.Mapper
{
    public class EnquiryProfile : Profile
    {
        public EnquiryProfile()
        {
            CreateMap<EnquiryModel, Enquiry>().ForMember(o => o.Id, opt => opt.Ignore()).ForMember(o => o.When, opt => opt.Ignore());
        }
    }
}
