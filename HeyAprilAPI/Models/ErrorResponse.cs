namespace HeyAprilAPI.Models
{
    public class ErrorResponse
    {
        public required string Error { get; set; }
        public List<string> Details { get; set; } = [];
    }
}
