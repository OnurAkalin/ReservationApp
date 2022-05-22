using Domain.Enumerations;

namespace Domain.Entities;

public class Component : EntityBase<int>
{
    public ComponentType Type { get; set; }
    public string Value { get; set; }
}