using System;

namespace Types.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime CreatedDateUtc { get; set; }
    }
}
