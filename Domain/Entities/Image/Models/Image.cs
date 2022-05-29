namespace Domain.Entities;

public class Image : EntityBase<int>
{
    public byte[] ImageContent { get; set; }
}