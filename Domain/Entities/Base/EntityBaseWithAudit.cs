namespace Domain.Entities;

public abstract class EntityBaseWithAudit<T> : EntityBase<T>
{
    public Guid? CreateUser { get; set; }
    public DateTime CreateDate { get; set; }
    public Guid? ModifyUser { get; set; }
    public DateTime? ModifyDate { get; set; }
}