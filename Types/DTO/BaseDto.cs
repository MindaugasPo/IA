using System;

namespace Types.DTO
{
    public class BaseDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedDateUtc { get; set; }
    }
}
