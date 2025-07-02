using HeyAprilAPI.Models;

namespace HeyAprilAPI.Interfaces
{
    public interface IIncidentRepository
    {
        Task<Incident> CreateAsync(Incident incident);
        Task<Incident?> GetByIdAsync(int id);
        Task<List<Incident>> GetAllAsync();
        Task<bool> IsDuplicateAsync(string title, string description, TimeSpan timeWindow);
    }
}
