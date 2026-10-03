// EventEase in-memory store + attendance tracker.
// Copilot drafted the dictionary pattern; I fixed routing keys and validation.
namespace EventEase.Services;

using EventEase.Models;

public class EventService
{
    private readonly List<EventItem> _events = new()
    {
        new EventItem { Id = 1, Title = "Tech Conference", Date = DateTime.Today.AddDays(7), Location = "Jakarta", Description = "Talks on cloud and AI." },
        new EventItem { Id = 2, Title = "Music Festival", Date = DateTime.Today.AddDays(14), Location = "Bandung", Description = "Live bands and food stalls." },
        new EventItem { Id = 3, Title = "Startup Meetup", Date = DateTime.Today.AddDays(21), Location = "Surabaya", Description = "Networking for founders." }
    };

    private readonly Dictionary<int, List<Registration>> _registrations = new();
    private readonly HashSet<(int EventId, string Email)> _attendance = new();

    public List<EventItem> GetEvents() => _events;
    public EventItem? GetEvent(int id) => _events.FirstOrDefault(e => e.Id == id);

    public (bool Ok, string Error) AddRegistration(Registration r)
    {
        if (r.EventId <= 0 || GetEvent(r.EventId) is null) return (false, "Invalid event.");
        if (string.IsNullOrWhiteSpace(r.Name) || r.Name.Trim().Length < 2) return (false, "Name min 2 chars.");
        if (!System.Text.RegularExpressions.Regex.IsMatch(r.Email ?? "", @"^[^\s@]+@[^\s@]+\.[^\s@]+$")) return (false, "Invalid email.");
        var list = _registrations.GetValueOrDefault(r.EventId, new List<Registration>());
        if (list.Any(x => x.Email.Equals(r.Email, StringComparison.OrdinalIgnoreCase))) return (false, "Already registered.");
        list.Add(new Registration { EventId = r.EventId, Name = r.Name.Trim(), Email = r.Email.Trim() });
        _registrations[r.EventId] = list;
        return (true, "");
    }

    public List<Registration> GetRegistrations(int eventId) =>
        _registrations.GetValueOrDefault(eventId, new List<Registration>());

    public int RegistrationCount(int eventId) => GetRegistrations(eventId).Count;

    public bool MarkAttended(int eventId, string email)
    {
        var key = (eventId, email.ToLowerInvariant());
        if (!GetRegistrations(eventId).Any(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase))) return false;
        _attendance.Add(key);
        return true;
    }

    public bool IsAttended(int eventId, string email) =>
        _attendance.Contains((eventId, email.ToLowerInvariant()));
}
