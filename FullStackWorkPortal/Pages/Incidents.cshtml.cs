using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FullStackWorkPortal.Models;

namespace FullStackWorkPortal.Pages;

public class IncidentsModel : PageModel
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<IncidentsModel> _logger;

    public IncidentsModel(IHttpClientFactory httpClientFactory, ILogger<IncidentsModel> logger)
    {
        _httpClient = httpClientFactory.CreateClient();
        _logger = logger;
    }

    public List<Incident> Incidents { get; set; } = new();

    public async Task OnGetAsync()
    {
        try
        {
            // API call happens here
            Incidents = await _httpClient.GetFromJsonAsync<List<Incident>>("https://localhost:7176/api/Incidents") ?? new();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch incidents");
        }
    }

    /// <summary>
    /// Maps an <see cref="IncidentSeverity"/> value to a corresponding CSS class name.
    /// </summary>
    /// <param name="severity">The severity level of the incident to map. Must be a valid <see cref="IncidentSeverity"/> value.</param>
    /// <returns>A string representing the CSS class name associated with the specified severity level: <list type="bullet">
    /// <item><description><c>"table-danger"</c> for <see cref="IncidentSeverity.High"/>.</description></item>
    /// <item><description><c>"table-warning"</c> for <see cref="IncidentSeverity.Medium"/>.</description></item>
    /// <item><description><c>"table-success"</c> for <see cref="IncidentSeverity.Low"/>.</description></item>
    /// <item><description>An empty string (<c>""</c>) for unrecognized severity values.</description></item> </list></returns>
    public string GetSeverityClass(IncidentSeverity severity) => severity switch
    {
        IncidentSeverity.High => "table-danger",
        IncidentSeverity.Medium => "table-warning",
        IncidentSeverity.Low => "table-success",
        _ => ""
    };
}
