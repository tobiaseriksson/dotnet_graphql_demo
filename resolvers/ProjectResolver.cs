

using com.nkt.npt.api.fake;

namespace com.nkt.npt.api.model {

    [ExtendObjectType(typeof(Project))]
    public class ProjectResolvers
    {
        FakeDB fakeDB;
        public ProjectResolvers(FakeDB _fakeDb) {
            fakeDB = _fakeDb;
        }

        public List<EventX> GetEvents([Parent] Project proj, int? limit=null)
        {
            var projId = proj.Id;
            if( limit != null && limit > 0 ) return fakeDB.events.Values.Where( evt => evt.BelongsToProjectId == projId ).Take((int)limit).ToList();
            return fakeDB.events.Values.Where( evt => evt.BelongsToProjectId == projId ).ToList();
        }


        public List<Resource> GetResources([Parent] Project proj)
        {
            return fakeDB.resources.Values.Where( res => res.belongsToProjectId == proj.Id ).ToList();
        }

        public DateOnly GetStart([Parent] Project proj) {
            var events = fakeDB.events.Values.Where( evt => evt.BelongsToProjectId == proj.Id ).AsQueryable();
            var start = events.Select( e => e.start ).Min();
            return start;
        }

        public DateOnly GetEnd([Parent] Project proj) {
            var events = fakeDB.events.Values.Where( evt => evt.BelongsToProjectId == proj.Id );
            var end = events.Select( e => e.start ).Max();
            return end;
        }

        public int GetDays([Parent] Project proj) {
            var events = fakeDB.events.Values.Where( evt => evt.BelongsToProjectId == proj.Id );
            var start = events.Select( e => e.start ).Min();
            var end = events.Select( e => e.start ).Max();
            var days = (end.DayNumber - start.DayNumber);
            return days;
        }
    }

}