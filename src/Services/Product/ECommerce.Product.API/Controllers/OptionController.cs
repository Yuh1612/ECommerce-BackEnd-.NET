using ECommerce.Products.API.Services;
using ECommerce.Products.API.ViewModels.Options.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Products.API.Controllers
{
    [Route("api/v1/sv2/[controller]s")]
    [ApiController]
    public class OptionController : ControllerBase
    {
        private readonly OptionService _optionService;

        public OptionController(OptionService optionService)
        {
            _optionService = optionService;
        }

        [HttpGet]
        public async Task<List<OptionInfoResponse>> GetOptions()
        {
            return await _optionService.GetOptions();
        }
    }
}