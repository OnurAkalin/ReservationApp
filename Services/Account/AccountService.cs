using Microsoft.AspNetCore.Http;

namespace Services.Account;

public class AccountService : BasicService
{
    public AccountService
    (
        ILogger logger,
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper
    ) : base(logger, httpContextAccessor, mapper)
    {
    }
}