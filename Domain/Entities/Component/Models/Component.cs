namespace Domain.Entities;

public class Component : EntityBaseSite<int>
{
    public ComponentType Type { get; set; }
    public string Value { get; set; }
}