namespace Services;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        #region User

        CreateMap<RegisterRequestDto, User>();

        #endregion

        #region Role

        CreateMap<Role, RoleResponseDto>();

        #endregion
    }
}