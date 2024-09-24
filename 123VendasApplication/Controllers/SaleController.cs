using _123Vendas.Domain.Models;
using _123Vendas.Domain.Services;
using _123VendasApplication.Constants;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace _123VendasApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly IMapper _mapper;
        public SaleController(ISaleService saleService, IMapper mapper)
        {
            _saleService = saleService;
            _mapper = mapper;

        }

        [HttpGet]
        [Route(UriTemplates.SALE)]
        public async Task<IActionResult> GetAllProjectsAsync()
        {
            var result = await _saleService.GetAllSaleAsync();
            return Ok(result);
        }
        [HttpGet]
        [Route(UriTemplates.SALE_FIND_BY_ID)]
        public async Task<IActionResult> GetSaleAsync([FromQuery] Guid id)
        {
            var result = await _saleService.GetSaleAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [Route(UriTemplates.SALE)]
        public async Task<IActionResult> CreateProjectAsync([FromBody] Sale sale)
        {
            await _saleService.SaveSaleAsync(sale);
            return Ok();
        }

        [HttpDelete]
        [Route(UriTemplates.SALE_FIND_BY_ID)]
        public async Task<IActionResult> DeleTaskAsync([FromQuery] Guid id)
        {
            await _saleService.DeleteSaleAsync(id);
            return Ok();
        }
    }
}
