using FullStackWorkAPI.Interfaces;
using FullStackWorkAPI.Models;

namespace FullStackWorkAPI.Repository
{
    public class InMemoryIncidentRepository : IIncidentRepository
    {
        private readonly List<Incident> _incidents = [];
        private int _nextId = 1;
        private readonly object _lock = new();

        public Task<Incident> CreateAsync(Incident incident)
        {
            lock (_lock)
            {
                incident.Id = _nextId++;
                _incidents.Add(incident);
                return Task.FromResult(incident);
            }
        }

        public Task<Incident?> GetByIdAsync(int id)
        {
            lock (_lock)
            {
                var incident = _incidents.Find(i => i.Id == id);
                return Task.FromResult(incident);
            }
        }

        public Task<List<Incident>> GetAllAsync()
        {
            lock (_lock)
            {
                return Task.FromResult(_incidents.ToList());
            }
        }

        public Task<bool> IsDuplicateAsync(string title, string description, TimeSpan timeWindow)
        {
            lock (_lock)
            {
                var cutoffTime = DateTime.UtcNow - timeWindow;
                var isDuplicate = _incidents.Exists(i =>
                    string.Equals(i.Title, title, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(i.Description, description, StringComparison.OrdinalIgnoreCase) &&
                    i.CreatedAt >= cutoffTime
                );

                return Task.FromResult(isDuplicate);
            }
        }
    }
}
