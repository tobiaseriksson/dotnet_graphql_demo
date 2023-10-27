

namespace com.nkt.npt.api.model;


public class Individual
{
    public String FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string JobTitle { get; set; }

    public String GetFullName() {
        return FirstName + " " + LastName;
    }
}