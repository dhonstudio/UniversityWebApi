using Application.DTO;
using Application.Features;
using Microsoft.AspNetCore.Mvc;

namespace UniversityWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController(StudentFeature _studentFeature) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _studentFeature.GetAllStudent();
            return Ok(result);
        }

        [HttpPost("ImportFromPlaceholder")]
        public async Task<IActionResult> ImportFromPlaceholder(PlaceholderImport import)
        {
            var result = await _studentFeature.ImportFromPlaceholder(import.IdPlaceholderUser);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
