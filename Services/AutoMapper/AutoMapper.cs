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

        CreateMap<ReservationRequestDto, Reservation>()
            .ForMember(x => x.BeforeStart, y => y.MapFrom(z => z.Resizable.BeforeStart))
            .ForMember(x => x.AfterEnd, y => y.MapFrom(z => z.Resizable.AfterEnd))
            .ForMember(x => x.Editable, y => y.MapFrom(z => z.Actions.Editable))
            .ForMember(x => x.Deletable, y => y.MapFrom(z => z.Actions.Deletable))
            .ForMember(x => x.UserMessage, y => y.MapFrom(z => z.Meta.UserMessage))
            .ForMember(x => x.UserId, y => y.MapFrom(z => z.Meta.UserId))
            .ForMember(x => x.SiteServiceId, y => y.MapFrom(z => z.Meta.SiteServiceId));

        CreateMap<Reservation, ReservationResponseDto>()
            .ForMember(x => x.Resizable.BeforeStart, y => y.MapFrom(z => z.BeforeStart))
            .ForMember(x => x.Resizable.AfterEnd, y => y.MapFrom(z => z.AfterEnd))
            .ForMember(x => x.Actions.Editable, y => y.MapFrom(z => z.Editable))
            .ForMember(x => x.Actions.Deletable, y => y.MapFrom(z => z.Deletable))
            .ForMember(x => x.Meta.UserMessage, y => y.MapFrom(z => z.UserMessage))
            .ForMember(x => x.Meta.User, y => y.MapFrom(z => z.User))
            .ForMember(x => x.Meta.SiteService, y => y.MapFrom(z => z.SiteService))
            .ForMember(x => x.Color, y => y.MapFrom(z => z.SiteService.Color));



        #endregion
    }
}