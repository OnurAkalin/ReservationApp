namespace Domain.Entities;

public class Image : EntityBase<int>
{
    public string ImageTitle { get; set; }
    public byte[] ImageData { get; set; }
}