# EventEase - Blazor Project (Copilot-assisted)

## How Copilot assisted each step (rubric 5pts)

- Activity 1 (foundational Event Card): Copilot generated `EventCard.razor` with `EventItem` fields and `@bind` two-way binding on Title. I kept the binding demo and added the required-field validation message.
- Activity 2 (debug + optimize): Copilot helped fix the `/events/{Id:int}` route binding (`OnParametersSet` + `GetEvent(Id)`), corrected the nav link `/events/@Event.Id`, and added input validation (name min 2, email regex, duplicate check) plus not-found handling to avoid routing errors.
- Activity 3 (advanced): Copilot scaffolded the Registration Form (name/email + submit), `SessionState` login/logout on Home, `EventService` registration store, and the Attendance Tracker (Mark present / Present-Absent). I wired counts and verified the flow.

## Run

```bash
dotnet run
# open https://localhost:5001
```

Routes: `/` (list + login), `/events/1` (details + register + attendance).
