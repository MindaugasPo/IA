using System.Collections.Generic;
using Types.DTO;

namespace IA.ViewModels
{
    public class AllTransactionsVM
    {
        public List<TransactionDto> Transactions { get; set; }
        public bool DisplayCloseData { get; set; }
    }
}
