using System;
using System.Collections.Generic;
using System.Text;

namespace Types.Entities
{
    public class Portfolio : EntityBase
    {
        public string Title { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
