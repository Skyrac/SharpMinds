namespace SharpMinds.SoftwarePattern.Builder;

public class PersonDirector
{
    private PersonBuilder _personBuilder = new();

    public Person ConstructMinimalPerson(string firstname, string lastname)
    {
        return _personBuilder
            .SetName($"{lastname} {firstname}")
            .Build();
    }
    
    public Person ConstructPersonWithAddress(string firstname, string lastname, string street, string city, string postalCode, string country)
    {
        return _personBuilder
            .SetName($"{lastname} {firstname}")
            .SetAddress($"{street}, {city}, {postalCode}, {country}")
            .Build();
    }
}