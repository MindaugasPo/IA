using System;
using IAapi.Filters;
using Microsoft.AspNetCore.Mvc;
using Services;
using Types.DTO;

namespace IAapi.Controllers
{
    [Route("apiv1/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioService _portfolioService;
        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        [HttpPost]
        [Route("create")]
        [ServiceFilter(typeof(IaValidationFilter))]
        public PortfolioDto Create(PortfolioDto portfolio)
        {
            return _portfolioService.Create(portfolio);
        }
    }
}
