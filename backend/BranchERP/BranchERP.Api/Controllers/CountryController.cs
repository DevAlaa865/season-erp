using BranchERP.Application.DTOs.Country;
using BranchERP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BranchERP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _countryService.GetAllAsync());

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged(
            int pageIndex = 1,
            int pageSize = 10,
            string? search = null)
            => Ok(await _countryService.GetPagedAsync(pageIndex, pageSize, search));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _countryService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CountryCreateUpdateDto model)
            => Ok(await _countryService.CreateAsync(model));

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CountryCreateUpdateDto model)
            => Ok(await _countryService.UpdateAsync(id, model));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _countryService.DeleteAsync(id));
    }
}
