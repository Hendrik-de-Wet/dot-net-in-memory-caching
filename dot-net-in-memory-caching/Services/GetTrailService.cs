using dot_net_core_in_memory_caching.Controllers;
using dot_net_core_in_memory_caching.Interfaces;
using dot_net_core_in_memory_caching.Models;
using Microsoft.Extensions.Hosting;

namespace dot_net_core_in_memory_caching.Services
{
  public class GetTrailService : ITrailService
  {
    private readonly ILogger<TrailController> _logger;

    public GetTrailService(ILogger<TrailController> logger)
    {
      _logger = logger;
    }

    public async Task<Trail> GetTrail(int Id)
    {
      try
      {
        // Sample Data
        var trails = new List<Trail>()
        {
            new Trail
            {
                Id = 1,
                Name = "Maltese Cross",
                Region = "Cederberg",
                Country = "South Africa",
                Latitude = "0",
                Longitude  = "0"
            },
            new Trail
            {
                Id = 2,
                Name = "Wolfberg Cracks and Arch",
                Region = "Cederberg",
                Country = "South Africa",
                Latitude = "0",
                Longitude  = "0"
            },
            new Trail
            {
                Id = 3,
                Name = "Stadsaal Caves",
                Region = "Cederberg",
                Country = "South Africa",
                Latitude = "0",
                Longitude  = "0"
            }
        };

        // Select 
        Trail trail = new Trail();
        trail = trails.FirstOrDefault(x => x.Id == Id);
        _logger.LogInformation($"Successfully retrieved information from data source for Id [{Id}]");
        return trail;
      }
      catch (Exception)
      {
        _logger.LogInformation($"Error retrieving information from data source.");
        throw;
      }
    }
  }
}
