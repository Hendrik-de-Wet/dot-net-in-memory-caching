using dot_net_core_in_memory_caching.Models;

namespace dot_net_core_in_memory_caching.Interfaces
{
  public interface ITrailService
  {
    Task<Trail> GetTrail(int Id);
  }
}
