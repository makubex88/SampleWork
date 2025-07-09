using FullStackWorkAPI.Enums;

namespace FullStackWorkAPI.Models
{
    public class Incident
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public IncidentSeverity Severity { get; set; }
        public string? ReporterName { get; set; }
        public string? ReporterEmail { get; set; }
        public DateTime CreatedAt { get; set; }
        public IncidentStatus Status { get; set; } = IncidentStatus.Open;

        public int CalculateUrgency()
        {
            // Base score according to severity
            int baseUrgency = Severity switch
            {
                IncidentSeverity.High => 100,
                IncidentSeverity.Medium => 60,
                IncidentSeverity.Low => 30,
                _ => 0
            };

            // Calculate age in hours
            var age = DateTime.UtcNow - CreatedAt;
            var ageHours = age.TotalHours;

            // Increase urgency based on age
            // +10 points for every 24 hours elapsed, up to +50 points
            int ageUrgency = Math.Min((int)(ageHours / 24) * 10, 50);

            return Math.Min(baseUrgency + ageUrgency, 150);
        }
    }
}
