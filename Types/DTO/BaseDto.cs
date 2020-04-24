using System;
using System.Collections.Generic;
using System.Text;

namespace Types.DTO
{
    public class BaseDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedDateUtc { get; set; }
    }
}
