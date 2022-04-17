using Core.Entities;

namespace Domain.Entities;

public class Image : EntityBase<int>, IEntity
{
    public byte[] ImageContent { get; set; }
}