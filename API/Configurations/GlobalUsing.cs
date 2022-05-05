global using Swashbuckle.AspNetCore.SwaggerGen;
global using Core.Encryption;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.OpenApi.Models;
global using Microsoft.IdentityModel.Tokens;
global using Infrastructure;
global using Microsoft.EntityFrameworkCore;
global using Services;
global using API.Configurations;
global using Domain.Entities;
global using Microsoft.AspNetCore.Identity;
global using Serilog;
global using Services.AutoMapper;
global using StackExchange.Redis;
global using Role = Domain.Entities.Role;
global using TokenOptions = Core.Jwt.TokenOptions;

