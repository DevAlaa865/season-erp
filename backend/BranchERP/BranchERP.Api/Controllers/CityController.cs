using BranchERP.Application.DTOs.City;
using BranchERP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BranchERP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _cityService.GetAllAsync());

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged(
            int pageIndex = 1,
            int pageSize = 10,
            string? search = null)
            => Ok(await _cityService.GetPagedAsync(pageIndex, pageSize, search));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _cityService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CityCreateUpdateDto model)
            => Ok(await _cityService.CreateAsync(model));

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CityCreateUpdateDto model)
            => Ok(await _cityService.UpdateAsync(id, model));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _cityService.DeleteAsync(id));
    }
}
