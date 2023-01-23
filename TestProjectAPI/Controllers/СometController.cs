using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TestProjectAPI.Services;

namespace TestProjectAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ÑometController : Controller
    {

        private readonly ILogger<ÑometController> _logger;
        private readonly IDataNasaServices _dataNasaServices;

        public ÑometController(ILogger<ÑometController> logger, IDataNasaServices dataNasaServices)
        {
            _logger = logger;
            _dataNasaServices = dataNasaServices;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<ActionResult> Get()
        {
            var a = await _dataNasaServices.GetComets();
            return Ok();
        }
    }
}