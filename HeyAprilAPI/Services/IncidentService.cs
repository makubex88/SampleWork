using HeyAprilAPI.Enums;
using HeyAprilAPI.Exceptions;
using HeyAprilAPI.Interfaces;
using HeyAprilAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace HeyAprilAPI.Services
{
    public class IncidentService : IIncidentService
    {
        private readonly IIncidentRepository _repository;

        public IncidentService(IIncidentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Incident> CreateIncidentAsync(CreateIncidentRequest request)
        {
            // Validate severity enum (additional safety check)
            if (!Enum.IsDefined(typeof(IncidentSeverity), request.Severity))
            {
                throw new ValidationException($"Invalid severity value. Allowed values: {string.Join(", ", Enum.GetNames<IncidentSeverity>())}");
            }

            // Check for duplicates within 24 hours
            var isDuplicate = await _repository.IsDuplicateAsync(
                request.Title.Trim(),
                request.Description.Trim(),
                TimeSpan.FromHours(24)
            );

            if (isDuplicate)
            {
                throw new DuplicateIncidentException(
                    $"An incident with the same title and description was already submitted within the last 24 hours"
                );
            }



            var incident = new Incident
            {
                Title = request.Title.Trim(),
                Description = request.Description.Trim(),
                Severity = request.Severity,
                ReporterName = request.ReporterName?.Trim(),
                ReporterEmail = request.ReporterEmail?.Trim().ToLowerInvariant(),
                CreatedAt = DateTime.UtcNow
            };

            return await _repository.CreateAsync(incident);
        }

        public async Task<Incident?> GetIncidentAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<Incident>> GetAllIncidentsAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
