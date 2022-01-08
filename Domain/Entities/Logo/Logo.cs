using Core.Entities;

namespace Domain.Entities
{
    public class Logo : EntityBase<int>, IEntity
    {
        public byte[] LogoContent { get; set; }
    }
}