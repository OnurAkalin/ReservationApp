namespace Domain.Entities;

public class AuthLayoutDto : IDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string BackgroundColor { get; set; }
    public string Font { get; set; }
    public string BackgroundImage { get; set; }
}