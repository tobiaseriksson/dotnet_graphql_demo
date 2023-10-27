

namespace com.nkt.npt.api.model;

public class EventX
{
    public string Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }

    public DateOnly start { get; set; }
    public DateOnly end { get; set; }

    public int GetDays()
    {
        return (int)end.DayNumber - start.DayNumber;
    }

    public String BelongsToProjectId { get; set; }

    public String BelongsToResourceId { get; set; }

    public  IEventDetails Details { get; set; }
}

