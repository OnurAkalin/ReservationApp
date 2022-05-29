namespace Domain.Entities;

public class CustomDto : IDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Layout { get; set; }
    public string TargetComponent { get; set; }
    public string Content { get; set; }
    public string Script { get; set; }
    public string DependentComponents { get; set; }
}