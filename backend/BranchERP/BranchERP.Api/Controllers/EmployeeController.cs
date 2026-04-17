using BranchERP.Application.DTOs.Employee;
using BranchERP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BranchERP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _employeeService.GetAllAsync());

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged(int pageIndex = 1, int pageSize = 10, string? search = null)
            => Ok(await _employeeService.GetPagedAsync(pageIndex, pageSize, search));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _employeeService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeCreateUpdateDto model)
            => Ok(await _employeeService.CreateAsync(model));

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeCreateUpdateDto model)
            => Ok(await _employeeService.UpdateAsync(id, model));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _employeeService.DeleteAsync(id));
    }
}
