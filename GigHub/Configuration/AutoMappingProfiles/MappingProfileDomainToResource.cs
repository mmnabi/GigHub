using AutoMapper;
using GigHub.Core.Models;
using GigHub.Core.Resources;

namespace GigHub.Configuration.AutoMappingProfiles
{
    public class MappingProfileDomainToResource : Profile
    {
        public MappingProfileDomainToResource()
        {
            // Domain to API Resource
            CreateMap<Genre, GenreResource>();
            CreateMap<ApplicationUser, ArtistResource>();
            CreateMap<Gig, GigResource>()
                .ForMember(gr => gr.Date, opt => opt.MapFrom(g => g.DateTime.ToString("dd MMM yyyy")))
                .ForMember(gr => gr.Time, opt => opt.MapFrom(g => g.DateTime.ToString("HH:mm")));
        }
    }
}
