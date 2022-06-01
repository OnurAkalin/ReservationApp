namespace Domain.Entities;

public class Image : EntityBase<int>
{
    public string Title { get; set; }
    public string Path { get; set; }
    public byte[] Data { get; set; }
}