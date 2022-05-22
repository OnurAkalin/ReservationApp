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

        #endregion
    }
}