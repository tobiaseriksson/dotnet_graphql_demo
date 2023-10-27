

using com.nkt.npt.api.fake;
using com.nkt.npt.api.model;

namespace com.nkt.npt.api.graphql;


[ExtendObjectType("Query")]
public class Queries
{

    FakeDB fakeDB;

    public Queries(FakeDB _fakeDB)
    {
        fakeDB = _fakeDB;
    }

    [GraphQLDescription("Returns all the Projects")]
    public IQueryable<Project> allProjects(string? nameContains = null, int? limit = null, DateOnly? from=null)
    {
        IQueryable<Project> result = fakeDB.projects.Values.AsQueryable();
        if (nameContains != null && nameContains.Trim().Length > 0)
        {
            result = result.AsQueryable().Where(o => o.Name.Contains(nameContains)).AsQueryable();
        }
        if( from != null ) {
            var projectsWithStartDate = fakeDB.events.Values.GroupBy( evt => evt.BelongsToProjectId ).Select( g => new {projectId = g.Key, startDate = g.Min(cm => cm.start)});
            var eligibleProjects = projectsWithStartDate.Where( tpl => from < tpl.startDate ).Select( tpl => tpl.projectId ).ToHashSet();            
            result = result.Where( proj => eligibleProjects.Contains( proj.Id ) ).AsQueryable();
        }
        if (limit != null) result = result.Take((int)limit);
        return result.OrderBy(o => o.Id);
    }


    [GraphQLDescription("Returns all the Events for a Project")]
    public IQueryable<EventX> allEvents(string? projectId = null, int? limit = null)
    {
        IQueryable<EventX> result = null;

        if (projectId != null && projectId.Trim().Length > 0)
        {
            result = fakeDB.events.Values.AsQueryable().Where(o => o.BelongsToProjectId == projectId).AsQueryable();
        }
        else
        {
            result = fakeDB.events.Values.AsQueryable();
        }
        if (limit != null) result = result.Take((int)limit);
        return result.OrderBy(o => o.Id);
    }


}


