using System.Reflection;
using System.Threading.Channels;

namespace FullStackWorkPortal.Models
{
    /// <summary>
    /// Represents an incident report, including details such as title, description, severity, and status.
    /// </summary>
    /// <remarks>This class is used to encapsulate information about an incident, including metadata such as
    /// the  reporter's name and email, the creation timestamp, and the current status of the incident.</remarks>
    public class Incident
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }

        // - Describe what could go wrong in rendering and how you'd fix it.
        // The changes:
        //1.	Changed the Severity property type from string to IncidentSeverity enum
        //2.	Updated the GetSeverityClass method to accept and handle the enum instead of a string
        //3.	Added the required enum definitions to the model file
        public IncidentSeverity Severity { get; set; }  // Changed from string to enum
        public string? ReporterName { get; set; }
        public string? ReporterEmail { get; set; }
        public DateTime CreatedAt { get; set; }
        public IncidentStatus Status { get; set; }
    }

    /// <summary>
    /// Represents the severity level of an incident.
    /// </summary>
    /// <remarks>This enumeration is used to classify incidents based on their level of severity. The severity
    /// levels are defined as follows: <list type="bullet"> <item> <term><see cref="Low"/></term> <description>Indicates
    /// a low-severity incident that requires minimal attention.</description> </item> <item> <term><see
    /// cref="Medium"/></term> <description>Indicates a medium-severity incident that requires moderate
    /// attention.</description> </item> <item> <term><see cref="High"/></term> <description>Indicates a high-severity
    /// incident that requires immediate attention.</description> </item> </list></remarks>
    public enum IncidentSeverity
    {
        Low,
        Medium,
        High
    }

    /// <summary>
    /// Represents the status of an incident in a tracking system.
    /// </summary>
    /// <remarks>This enumeration defines the possible states an incident can be in during its lifecycle:
    /// <list type="bullet"> <item> <description><see cref="Open"/>: The incident has been reported but no action has
    /// been taken yet.</description> </item> <item> <description><see cref="InProgress"/>: The incident is currently
    /// being investigated or addressed.</description> </item> <item> <description><see cref="Resolved"/>: The incident
    /// has been resolved, but it may still require confirmation or follow-up.</description> </item> <item>
    /// <description><see cref="Closed"/>: The incident has been fully resolved and is no longer active.</description>
    /// </item> </list></remarks>
    public enum IncidentStatus
    {
        Open,
        InProgress,
        Resolved,
        Closed
    }
}
