global using AutoMapper;
global using Microsoft.AspNetCore.Http;
global using User = Domain.Entities.User;
using Infrastructure;
using Serilog.Core;

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