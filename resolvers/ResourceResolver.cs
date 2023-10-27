
using com.nkt.npt.api.fake;

namespace com.nkt.npt.api.model {

    [ExtendObjectType(typeof(Resource))]
    public class ResourceResolvers
    {
        FakeDB fakeDB;
        public ResourceResolvers(FakeDB _fakeDb) {
            fakeDB = _fakeDb;
        }
        
        [BindMember(nameof(Resource.parentId))]
        public Resource? parentResource([Parent] Resource res)
        {            
            if( res.parentId == null ) return null;
            return fakeDB.resources[res.parentId];            
        }

        public IQueryable<Resource> GetSubResources([Parent] Resource area, string? nameContains = null) {
            var result = fakeDB.resources.Values.Where( a => a.parentId == area.Id ).AsQueryable();
            if( nameContains != null && nameContains.Trim().Length > 0 ) {
                result = result.Where( a => a.Name().ToLower().Contains(nameContains.ToLower())).AsQueryable();
            }
            return result;
        }

        [BindMember(nameof(Resource.belongsToProjectId))]
        public Project? BelongsToPlan([Parent] Resource res)
        {            
            return fakeDB.projects[res.belongsToProjectId];
        }

        public IQueryable<EventX> GetEvents([Parent] Resource res,int? limit = null, DateOnly? from=null) {
            var result =  fakeDB.events.Values.Where( e => e.BelongsToResourceId == res.Id ).AsQueryable();
            if( from != null ) {
                result = result.Where( e => from < e.start  ).AsQueryable();
            }
            if (limit != null) result = result.Take((int)limit);
            return result;
        }
    }

}