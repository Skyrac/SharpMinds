using SharpMinds.SoftwarePattern.Builder;

namespace SharpMinds.Unit.Tests.SoftwarePattern.Builder;

public class PersonBuilderTests
{
    private PersonBuilder _builder = new();
    private const string Firstname = "Max";
    private const string Lastname = "Mustermann";
    private const string Name = $"{Lastname} {Firstname}";
    private const string Address = "Musterweg 17, 27127, Musterhausen, Deutschland";
    private const int Age = 29;
    private const string Job = "Technical Lead";
    
    [Test]
    public async Task Should_SetName_When_SetName_IsCalled()
    {
        _builder.SetName(Name);
        var person = _builder.Build();
        
        await Assert.That(person.Name).IsEqualTo(Name);
    }
    
    [Test]
    public async Task ShouldNot_SetName_When_SetAddress_IsCalled()
    {
        _builder.SetAddress(Name);
        var person = _builder.Build();
        
        await Assert.That(person.Name).IsNotEqualTo(Name);
    }
    
    [Test]
    public async Task Should_SetAddress_When_SetAddress_IsCalled()
    {
        _builder.SetAddress(Address);
        var person = _builder.Build();
        
        await Assert.That(person.Address).IsEqualTo(Address);
    }
    
    [Test]
    public async Task Should_SetAge_When_SetAge_IsCalled()
    {
        _builder.SetAge(Age);
        var person = _builder.Build();
        
        await Assert.That(person.Age).IsEqualTo(Age);
    }
    [Test]
    
    public async Task Should_SetJob_When_SetJob_IsCalled()
    {
        _builder.SetJob(Job);
        var person = _builder.Build();
        
        await Assert.That(person.Job).IsEqualTo(Job);
    }
}