using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HeyAprilMobile.Models
{
 
    public class Incident
    {
        [JsonPropertyName("incident_title")]
        public string Title { get; set; }

        [JsonPropertyName("incident_description")]
        public string Description { get; set; }

        [JsonPropertyName("incident_severity")]
        public string Severity { get; set; }
        [JsonPropertyName("reporterName")]
        public string ReporterName { get; set; }

        [JsonPropertyName("reporterEmail")]
        public string ReporterEmail { get; set; }
    }
}
