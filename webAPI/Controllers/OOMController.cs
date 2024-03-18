using Microsoft.AspNetCore.Mvc;

namespace webAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OOMController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private IList<string> strings = new List<string>();
        private int count = Int32.MaxValue - 10;

        public OOMController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetOOM")]
        public IActionResult Get()
        {
            for(int i = 0; i < count; i++)
            {
                strings.Add("0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789");
            }
            return Ok();
        }
    }
}
