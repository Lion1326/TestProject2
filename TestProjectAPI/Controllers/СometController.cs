using Microsoft.AspNetCore.Mvc;
using TestProject.App.CometManagement;
using TestProjectApp.CometManagement.Models;

namespace TestProjectAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CometController : Controller
{

    private readonly ILogger<CometController> _logger;
    private readonly IWorkService _workService;

    public CometController(ILogger<CometController> logger, IWorkService workService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
        _workService = workService ?? throw new ArgumentNullException(nameof(_workService));
    }
    [HttpGet("ClearDB")]
    public ActionResult ClearDB()
    {
        _workService.ClearDB();
        return Ok();
    }
    [HttpGet("LoadComets")]
    public async Task<ActionResult> UploadComets()
    {
        await _workService.SaveDataFromNasaAsync();
        return Ok();
    }

    [HttpPost("GetCometsByYear")]
    public async Task<ActionResult> GetComets([FromBody] GetCometsByYearRequest request)
    {
        var result = await _workService.GetCometsByYearAsync(request);
        return Json(result);
    }
    [HttpGet("GetProperties")]
    public async Task<ActionResult> GetPropertiesPage()
    {
        var result = await _workService.GetPropertiesAsync();
        return Json(result);
    }
}