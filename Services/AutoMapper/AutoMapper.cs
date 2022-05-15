namespace Services;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        #region User

        CreateMap<RegisterRequestDto, User>();
        CreateMap<UserDto, User>().ReverseMap();

        #endregion

        #region Role

        CreateMap<Role, RoleResponseDto>();

        #endregion
    }
}