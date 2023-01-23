using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TestProjectAPI.Services;

namespace TestProjectAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class �ometController : Controller
    {

        private readonly ILogger<�ometController> _logger;
        private readonly IDataNasaServices _dataNasaServices;

        public �ometController(ILogger<�ometController> logger, IDataNasaServices dataNasaServices)
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