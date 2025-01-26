namespace SharpMinds.Integration.Tests.DomainEvents;

[CollectionDefinition(nameof(PostgresDomainEventTestCollection))]
public class PostgresDomainEventTestCollection : ICollectionFixture<PostgresDomainEventTestDatabaseFixture>
{
    
}