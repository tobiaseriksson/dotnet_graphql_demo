

using Bogus;
using com.nkt.npt.api.model;

namespace com.nkt.npt.api.fake
{

    public class FakeDB
    {

        public Dictionary<string, Project> projects = new Dictionary<string, Project>();
        public Dictionary<string, Resource> resources = new Dictionary<string, Resource>();
        public Dictionary<string, EventX> events = new Dictionary<string, EventX>();

        private Faker faker = new Faker("sv");
        private Random rnd = new Random();

        public FakeDB()
        {
        }

        private string uuid() {            
            return Guid.NewGuid().ToString();
        }

        private Individual randomIndividual() {
            var firstName = faker.Name.FirstName();
                var lastName = faker.Name.LastName();
                var email = faker.Internet.Email(firstName=firstName,lastName=lastName);
                var phoneNumber = faker.Phone.PhoneNumber();
                var job = faker.Name.JobTitle();
                var indv  = new Individual() { FirstName = firstName, LastName = lastName, Email = email, PhoneNumber = phoneNumber, JobTitle = job};
            return indv;
        }

        public void createFakeData()
        {

            /**
            Projects ---
            */
            for( var i = 0; i < 30; i++) {
                var contact = randomIndividual();
                var proj = new Project() { Id = uuid(), Name = faker.Address.City()+"-Project", Contact = contact, Color = faker.Commerce.Color(), Account = faker.Finance.Account() };
                projects.Add(proj.Id, proj);
                /**
                Resources --- 
                */
                var numberOfResources = rnd.Next(1, 10);
                for (int r = 0; r < numberOfResources; r++)
                {
                    var indv = randomIndividual();
                    var res = new Resource() { Id = uuid(), contact = indv, belongsToProjectId = proj.Id };
                    resources.Add( res.Id, res);
                    createEvents(proj.Id,res.Id);
                }
            }            
        }

        void createEvents( string projectId, string resourceId ) {
            /**
            Events ---
            */
            var start = DateOnly.FromDateTime(DateTime.Now);
            start = start.AddDays(rnd.Next(-60,60));
            var numberOfEvents = rnd.Next(1,10);
            for (int i = 0; i < 10; i++)
            {
                var lengthInDays = rnd.Next(1, 5);
                var end = start.AddDays(lengthInDays);
                IEventDetails eventDetails = null;
                if (rnd.Next(1, 100) % 2 == 0)
                {
                    eventDetails = new SpecialDetails() { Id = uuid(), Description = faker.Lorem.Sentence(), SpecialInformation = "secret info" };
                }
                else
                {
                    eventDetails = new ToolDetails() { Id = uuid(), Name = "Manual", Description="hammer, screedriver, ..." };
                }

                EventX eventX = new EventX()
                {
                    Id = uuid(),
                    Name = "evt-" + i,
                    start = start,
                    end = end,
                    BelongsToProjectId = projectId,
                    BelongsToResourceId = resourceId,
                    Details = eventDetails
                };

                events.Add(eventX.Id, eventX);
                start = end;
            }
        }

    }
}