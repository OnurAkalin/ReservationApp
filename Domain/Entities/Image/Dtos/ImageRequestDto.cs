using Microsoft.AspNetCore.Http;

namespace Domain.Entities;

public class ImageRequestDto
{
    public IFormFile Image { get; set; }
}