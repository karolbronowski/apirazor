using AutoMapper;
using FinalLabProject.Domain.Entities;
using FinalLabProject.Application.Artists.Queries;
using FinalLabProject.Application.Songs.Queries;
using FinalLabProject.Application.Listeners.Queries;
using FinalLabProject.Domain.ValueObjects;

namespace FinalLabProject.Application.Common.Mappings;

public class DomainMappingProfile : Profile
{
    public DomainMappingProfile()
    {
        CreateMap<Artist, ArtistDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username.Value))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value))
            .ForMember(dest => dest.PayoutTier, opt => opt.MapFrom(src => src.PayoutTier.Name));

        CreateMap<Song, SongDto>();

        CreateMap<Listener, ListenerDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username.Value))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value));

        CreateMap<Username, string>().ConvertUsing(u => u.Value);
        CreateMap<EmailAddress, string>().ConvertUsing(e => e.Value);
        CreateMap<PayoutTier, string>().ConvertUsing(p => p.Name);
    }
}