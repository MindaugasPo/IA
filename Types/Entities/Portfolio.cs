﻿using System.Collections.Generic;

namespace Types.Entities
{
    public class Portfolio : EntityBase
    {
        public string Title { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
