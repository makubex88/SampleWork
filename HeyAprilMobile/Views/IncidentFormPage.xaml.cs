using System.Net.Http.Json;
using HeyAprilMobile.Models;

namespace HeyAprilMobile.Views;

public partial class IncidentFormPage : ContentPage
{
    private readonly HttpClient _httpClient;

    public IncidentFormPage()
    {
        InitializeComponent();
        _httpClient = new HttpClient();
    }

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        var incident = new Incident
        {
            Title = TitleEntry.Text?.Trim(),
            Description = DescriptionEditor.Text?.Trim(),
            Severity = SeverityPicker.SelectedItem?.ToString(),
            ReporterName = "Mobile-User",
            ReporterEmail = "mobileUser@email.com"
        };

        try
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7176/api/incidents", incident);

            if (response.IsSuccessStatusCode)
            {
                ResponseLabel.Text = "Incident submitted successfully!";
                ResponseLabel.TextColor = Colors.Green;
            }
            else
            {
                ResponseLabel.Text = $"Submission failed: {response.StatusCode}";
                ResponseLabel.TextColor = Colors.Red;
            }

            ResponseLabel.IsVisible = true;
        }
        catch (Exception ex)
        {
            ResponseLabel.Text = $"Error: {ex.Message}";
            ResponseLabel.TextColor = Colors.Red;
            ResponseLabel.IsVisible = true;
        }
    }
}
