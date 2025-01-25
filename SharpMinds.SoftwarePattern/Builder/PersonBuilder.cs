namespace SharpMinds.SoftwarePattern.Builder;

public class PersonBuilder
{
    private readonly Person _person = new();

    public PersonBuilder SetName(string name)
    {
        _person.Name = name;
        return this;
    }

    public PersonBuilder SetAge(int age)
    {
        _person.Age = age;
        return this;
    }

    public PersonBuilder SetAddress(string address)
    {
        _person.Address = address;
        return this;
    }

    public PersonBuilder SetJob(string job)
    {
        _person.Job = job;
        return this;
    }

    public PersonBuilder SetPhone(string phone)
    {
        _person.Phone = phone;
        return this;
    }

    public Person Build()
    {
        return _person;
    }
}