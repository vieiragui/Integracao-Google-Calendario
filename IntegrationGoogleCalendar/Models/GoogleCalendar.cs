namespace IntegrationGoogleCalendar.Models;

public class GoogleCalendar
{
    public string Summary { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}

public class GoogleQuickCalendar
{
    public string PrimaryId { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}