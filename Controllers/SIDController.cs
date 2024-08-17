using Mercury.Services;
using Microsoft.AspNetCore.Mvc;

namespace Mercury.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SIDController : ControllerBase
    {
        private readonly SqlService _sqlService;

        public SIDController(SqlService sqlService)
        {
            _sqlService = sqlService;
        }

        [HttpGet("get-news-id")]
        public async Task<IActionResult> GetNewsId(String tableName, int returnSID)
        {
            try
            {
                int newsId = await _sqlService.ExecuteStoredProcedureAsync(tableName, returnSID);
                return Ok(newsId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
