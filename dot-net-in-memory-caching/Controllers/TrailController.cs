using dot_net_core_in_memory_caching.Interfaces;
using dot_net_core_in_memory_caching.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;


namespace dot_net_core_in_memory_caching.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class TrailController : ControllerBase
  {

    private readonly ILogger<TrailController> _logger;
    private IMemoryCache _memoryCache;
    private ITrailService _trailService;
    private IConfiguration _config;

    public TrailController(ILogger<TrailController> logger, IMemoryCache memoryCache, ITrailService trailService, IConfiguration configuration)
    {
      _logger = logger;
      _memoryCache = memoryCache;
      _trailService = trailService;
      _config = configuration;
    }

    private readonly string id = "id";

    [HttpGet("{Id}")]
    [ResponseCache(VaryByQueryKeys = new[] { "id" }, Duration = 30)]
    public async Task<ActionResult<Trail>> GetTrail(int Id)
    {
      var slidingExpiration = _config.GetValue<double>("InMemoryCache:SlidingExpiration"); // Get time outs from App Settings.
      var AbsoluteExpiration = _config.GetValue<double>("InMemoryCache:AbsoluteExpiration");

      if (_memoryCache.TryGetValue("trail", out Trail trail)) // Do not get information if it is already in In-Memory Cache. 
      {
        _logger.LogInformation($"Successfully retrieved information from In-memory cache for Id [{Id}]");
        return Ok(trail);
      }
      else
      {
        trail = await _trailService.GetTrail(Id); // Get information from service.
        var cacheEntryOptions = new MemoryCacheEntryOptions() 
        .SetSlidingExpiration(TimeSpan.FromSeconds(slidingExpiration)) // The cache will expire after a particular time only if it has not been used during that time span.
        .SetAbsoluteExpiration(TimeSpan.FromSeconds(AbsoluteExpiration)) // The cache will expire after a particular time irrespective of the fact whether it has been used or not.
        .SetPriority(CacheItemPriority.Normal)
        .SetSize(1024);
        _memoryCache.Set(id, trail, cacheEntryOptions); // Add information to In-Memory Cache.
        _logger.LogInformation($"Successfully stored information for Id [{Id}] in In-memory cache.");
        return Ok(trail);
      }
    }
  }
}