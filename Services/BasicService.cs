namespace Services;

public class BasicService
{
    protected readonly Logger _logger;
    protected readonly IMapper _mapper;
    protected readonly ApplicationDbContext _dbContext;

    protected BasicService
    (
        Logger logger,
        IMapper mapper,
        ApplicationDbContext dbContext
    )
    {
        _logger = logger;
        _mapper = mapper;
        _dbContext = dbContext;
    }
}