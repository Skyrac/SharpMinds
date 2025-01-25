using SharpMinds.SoftwarePattern.Builder;

namespace SharpMinds.Unit.Tests.SoftwarePattern.Builder;

public class PersonDirectorTests
{
    private PersonDirector _director = new();
    private const string Firstname = "Max";
    private const string Lastname = "Mustermann";
    private const string Name = $"{Lastname} {Firstname}";
    private const string Street = "Musterweg 17";
    private const string City = "Musterhausen";
    private const string PostalCode = "27127";
    private const string Country = "Deutschland";
    private const string Address = $"{Street}, {City}, {PostalCode}, {Country}";
    
    [Test]
    public async Task Should_SetName_When_ConstructMinimalPerson_IsCalled()
    {
        var person = _director.ConstructMinimalPerson(Firstname, Lastname);
        
        await Assert.That(person.Name).IsEqualTo(Name);
    }
    
    [Test]
    public async Task Should_SetNameAndAddress_When_ConstructPersonWithAddress_IsCalled()
    {
        var person = _director.ConstructPersonWithAddress(Firstname, Lastname, Street, City, PostalCode, Country);
        
        await Assert.That(person.Name).IsEqualTo(Name);
        await Assert.That(person.Address).IsEqualTo(Address);
    }
    
   
}