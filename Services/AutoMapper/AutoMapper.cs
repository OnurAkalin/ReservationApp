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
        
        CreateMap<SiteServiceRequestDto, Domain.Entities.SiteService>();
        CreateMap<Domain.Entities.SiteService, SiteServiceResponseDto>();

        #endregion

        #region Reservation

        CreateMap<ReservationRequestDto, Reservation>()
            .ForPath(x => x.BeforeStart, y => y.MapFrom(z => z.Resizable.BeforeStart))
            .ForPath(x => x.AfterEnd, y => y.MapFrom(z => z.Resizable.AfterEnd))
            .ForPath(x => x.Editable, y => y.MapFrom(z => z.Actions.Editable))
            .ForPath(x => x.Deletable, y => y.MapFrom(z => z.Actions.Deletable))
            .ForPath(x => x.UserMessage, y => y.MapFrom(z => z.Meta.UserMessage))
            .ForPath(x => x.UserId, y => y.MapFrom(z => z.Meta.UserId))
            .ForPath(x => x.SiteServiceId, y => y.MapFrom(z => z.Meta.SiteServiceId));

        CreateMap<Reservation, ReservationResponseDto>()
            .ForPath(x => x.Resizable.BeforeStart, y => y.MapFrom(z => z.BeforeStart))
            .ForPath(x => x.Resizable.AfterEnd, y => y.MapFrom(z => z.AfterEnd))
            .ForPath(x => x.Actions.Editable, y => y.MapFrom(z => z.Editable))
            .ForPath(x => x.Actions.Deletable, y => y.MapFrom(z => z.Deletable))
            .ForPath(x => x.Meta.UserMessage, y => y.MapFrom(z => z.UserMessage))
            .ForPath(x => x.Meta.User, y => y.MapFrom(z => z.User))
            .ForPath(x => x.Meta.SiteService, y => y.MapFrom(z => z.SiteService))
            .ForPath(x => x.Color, y => y.MapFrom(z => z.SiteService.Color));

        #endregion
    }
}