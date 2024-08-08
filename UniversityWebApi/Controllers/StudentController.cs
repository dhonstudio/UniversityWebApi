using Application.DTO;
using Application.Features;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using Sieve.Services;

namespace UniversityWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController(StudentFeature _studentFeature, ISieveProcessor _sieveProcessor) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] SieveModel sieveModel)
        {
            var result = await _studentFeature.GetAllStudent(sieveModel);
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
