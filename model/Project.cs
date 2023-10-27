

namespace com.nkt.npt.api.model;

public class Project {
        public string Id { get; set; }
        public string Name { get; set; }    

        public string Color { get; set; }
        
        public string Account { get; set; }

        public Individual? Contact { get; set; }
    }
