using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialDataClient
{
    public interface IFinDataClient
    {

    }
    public class FinDataClient : IFinDataClient
    {
        private readonly string _apiKey;
        public FinDataClient(string apiKey)
        {
            _apiKey = apiKey;
        }
    }
}
