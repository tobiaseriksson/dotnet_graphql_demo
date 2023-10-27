

namespace com.nkt.npt.api.model;

public class Resource
{
    public string Id { get; set; }
    public Individual contact { get; set; }

    public string Name() {
        return contact.GetFullName();
    }
    public string parentId { get; set; }

    public string belongsToProjectId { get; set; }
}