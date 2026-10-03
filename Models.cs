// EventEase models - Copilot suggested property layout; reviewed manually.
namespace EventEase.Models;

public class EventItem
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public DateTime Date { get; set; } = DateTime.Today;
    public string Location { get; set; } = "";
    public string Description { get; set; } = "";
}

public class Registration
{
    public int EventId { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
}

public class SessionState
{
    public string? UserName { get; set; }
    public bool IsLoggedIn => !string.IsNullOrWhiteSpace(UserName);
}
