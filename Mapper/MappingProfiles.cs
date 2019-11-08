using AutoMapper;
using PgccApi.Entities;
using PgccApi.Models;
using PgccApi.Models.ViewModels;
using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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

            CreateMap<CompetitionPostModel, Competition>()
                .ForMember(o => o.Id, opt => opt.Ignore());

            CreateMap<SeasonModel, Season>()
                .ForMember(o => o.Id, opt => opt.Ignore());

            // Outbound
            CreateMap<Competition, CompetitionViewModel>();
            CreateMap<Enquiry, EnquiryViewModel>();
            CreateMap<Fixture, FixtureViewModel>();
            CreateMap<NewsItem, NewsItemViewModel>();
            CreateMap<Rink, RinkViewModel>();
            CreateMap<Season, SeasonViewModel>();
            CreateMap<User, UserViewModel>();
        }
    }
}
