using FullStackWorkAPI.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FullStackWorkAPI.Models
{
    public class CreateIncidentRequest
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 200 characters")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 1000 characters")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "Severity is required")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public required IncidentSeverity Severity { get; set; }

        [StringLength(100, ErrorMessage = "Reporter name cannot exceed 100 characters")]
        public string? ReporterName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? ReporterEmail { get; set; }
    }
}
