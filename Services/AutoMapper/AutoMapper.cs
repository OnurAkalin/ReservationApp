namespace Services;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        #region User

        CreateMap<RegisterRequestDto, User>();
        CreateMap<User, UserResponseDto>();
        CreateMap<User, EmployeeResponseDto>();
        CreateMap<EmployeeRequestDto, User>();

        #endregion

        #region Role

        CreateMap<Role, RoleResponseDto>();

        #endregion

        #region Site

        CreateMap<SiteRequestDto, Site>();
        CreateMap<Site, SiteResponseDto>();
        
        CreateMap<SiteServiceRequestDto, SiteService>();
        CreateMap<SiteService, SiteServiceResponseDto>();

        #endregion

        #region Reservation

        CreateMap<ReservationMainDto, Reservation>()
            .ForMember(x => x.BeforeStart, y => y.MapFrom(z => z.Resizable.BeforeStart))
            .ForMember(x => x.AfterEnd, y => y.MapFrom(z => z.Resizable.AfterEnd))
            .ForMember(x => x.Editable, y => y.MapFrom(z => z.Actions.Editable))
            .ForMember(x => x.Deletable, y => y.MapFrom(z => z.Actions.Deletable))
            .ForMember(x => x.UserMessage, y => y.MapFrom(z => z.Meta.UserMessage))
            .ForMember(x => x.UserId, y => y.MapFrom(z => z.Meta.UserId))
            .ForMember(x => x.SiteId, y => y.MapFrom(z => z.Meta.SiteId))
            .ForMember(x => x.SiteServiceId, y => y.MapFrom(z => z.Meta.SiteServiceId))
            .ReverseMap();

        #endregion
    }
}