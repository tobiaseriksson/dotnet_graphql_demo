
using com.nkt.npt.api.fake;

namespace com.nkt.npt.api.model {

    [ExtendObjectType(typeof(EventX))]
    public class EventXResolvers
    {
        FakeDB fakeDB;
        public EventXResolvers(FakeDB _fakeDb) {
            fakeDB = _fakeDb;
        }
        
        [BindMember(nameof(EventX.BelongsToProjectId))]
        public Project? BelongsToProject([Parent] EventX eventX)
        {
            var planId = eventX.BelongsToProjectId;
            return fakeDB.projects[planId];            
        }

        [BindMember(nameof(EventX.BelongsToResourceId))]
        public Resource? BelongsToResource([Parent] EventX eventX)
        {
            var areaId = eventX.BelongsToResourceId;
            return fakeDB.resources[areaId];
        }
        
    }

}