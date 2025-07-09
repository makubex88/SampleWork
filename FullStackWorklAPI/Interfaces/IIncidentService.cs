using FullStackWorkAPI.Models;

namespace FullStackWorkAPI.Interfaces
{
    public interface IIncidentService
    {
        Task<Incident> CreateIncidentAsync(CreateIncidentRequest request);
        Task<Incident?> GetIncidentAsync(int id);
        Task<List<Incident>> GetAllIncidentsAsync();
    }
}
