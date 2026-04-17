using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace BranchERP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ShortageAttachmentsController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public ShortageAttachmentsController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var uploadsRoot = Path.Combine(_env.WebRootPath, "uploads", "shortages");
            Directory.CreateDirectory(uploadsRoot);

            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadsRoot, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var relativePath = $"uploads/shortages/{fileName}";

            return Ok(new
            {
                success = true,
                path = relativePath
            });
        }
    }
}
