using Microsoft.AspNetCore.Http;

namespace Domain.Entities;

public class ImageRequestDto
{
    public int EntityId { get; set; }
    public IFormFile Image { get; set; }
}