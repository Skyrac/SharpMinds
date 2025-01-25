namespace SharpMinds.SoftwarePattern.Builder;

public class Person
{
    public string Name { get; internal set; } = "";
    public int Age { get; internal set; }
    public string Address { get; internal set; } = "";
    public string? Job { get; internal set; }
    public string? Phone { get; internal set; }

    public Person() { }
    
    public Person(string name, int age, string address, string? job = null, string? phone = null)
    {
        Name = name;
        Age = age;
        Address = address;
        Job = job;
        Phone = phone;
    }

    public override string ToString()
    {
        return $"Name: {Name}, Age: {Age}, Address: {Address}, Job: {Job}, Phone: {Phone}";
    }
}