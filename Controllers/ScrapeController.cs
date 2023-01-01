
using Microsoft.AspNetCore.Mvc;
using scraperor_v2.Dtos;
using scraperor_v2.Services;

namespace scraperor_v2.Controllers;


[ApiController]
[Route("api/scrape")]
public class ScrapeController : ControllerBase
{
    private readonly ILogger<ScrapeController> _logger;
    private readonly IScrapeService _scrapeService;

    public ScrapeController(ILogger<ScrapeController> logger, IScrapeService scrapeService)
    {
        _logger = logger;
        _scrapeService = scrapeService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { success = true,message = "It works, to do some scraping change method to POST XD" });
    }

    [HttpPost]
    public async Task<ActionResult<string>> Post([FromBody] ScrapeBodyDto body)
    {
        try
        {
            string[] contents = await _scrapeService.ScrapeAsync(body.Website!,body.Pointer!);
            return Ok(new ScrapeResponseBodyDto { success = true, contents = contents });
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("or not known")) return BadRequest(new
            {
                message = "The website url is invalid / not reachable"
            });
            return Ok(new ScrapeResponseBodyDto { success = false, contents = null });
        }
    }
}

