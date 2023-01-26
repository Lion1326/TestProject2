using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TestProject.Infrastructure.Models;
using TestProject.Infrastructure;
using TestProject.Services;
using TestProject.Services.Models;
using TestProject.App.CometManagement;
using TestProjectApp.CometManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace TestProjectAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ÑometController : Controller
    {

        private readonly ILogger<ÑometController> _logger;
        private readonly IWorkService _workService;

        public ÑometController(ILogger<ÑometController> logger, IWorkService workService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
            _workService = workService ?? throw new ArgumentNullException(nameof(_workService));
        }

        [HttpGet("UploadComets")]
        public async Task<ActionResult> UploadComets()
        {
            await _workService.SaveDataFromNasaAsync();
            return Ok();
        }

        [HttpPost("GetComets")]
        public async Task<ActionResult> GetComets([FromBody]GetCometsByYearRequest request)
        {
            var result =  await _workService.GetCometsByYearAsync(request);
            return Json(result);
        }
        [HttpPost("GetProperties")]
        public async Task<ActionResult> GetPropertiesPage()
        {
            var result = await _workService.GetPropertiesAsync();
            return Json(result);
        }
    }
}