namespace FullStackWorkAPI.Exceptions
{
    // Custom Exceptions
    public class DuplicateIncidentException : Exception
    {
        public DuplicateIncidentException(string message) : base(message) { }
    }
}
