using AutoMapper;
using GigHub.Core.Models;
using GigHub.Core.Resources;

namespace GigHub.Configuration.AutoMappingProfiles
{
    public class MappingProfileResourceToDomain : Profile
    {
        public MappingProfileResourceToDomain()
        {
            // API Resource to Domain
            CreateMap<SaveGigResource, Gig>()
                .ForMember(g => g.DateTime, opt => opt.MapFrom(gr => gr.GetDateTime()));
            CreateMap<GenreResource, Genre>();
        }
    }
}
