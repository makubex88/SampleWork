using HeyAprilAPI.Models;

namespace HeyAprilAPI.Interfaces
{
    public interface IIncidentService
    {
        Task<Incident> CreateIncidentAsync(CreateIncidentRequest request);
        Task<Incident?> GetIncidentAsync(int id);
        Task<List<Incident>> GetAllIncidentsAsync();
    }
}
