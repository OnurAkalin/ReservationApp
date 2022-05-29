namespace Domain.Entities;

public abstract class EntityBase<T> : IEntity
{
    public T Id { get; set; }
}