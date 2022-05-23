namespace Domain.Entities;

public abstract class EntityBaseWithAudit<T> : EntityBase<T>
{
    public int? CreateUser { get; set; }
    public DateTime CreateDate { get; set; }
    public int? ModifyUser { get; set; }
    public DateTime? ModifyDate { get; set; }
}