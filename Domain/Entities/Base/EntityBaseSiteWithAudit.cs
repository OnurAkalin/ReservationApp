namespace Domain.Entities;

public abstract class EntityBaseSiteWithAudit<T> : EntityBaseSite<T>
{
    public int? CreateUser { get; set; }
    public DateTime CreateDate { get; set; }
    public int? ModifyUser { get; set; }
    public DateTime? ModifyDate { get; set; }
}