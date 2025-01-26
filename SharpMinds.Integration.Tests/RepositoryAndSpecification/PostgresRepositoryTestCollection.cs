namespace SharpMinds.Integration.Tests.RepositoryAndSpecification;

[CollectionDefinition(nameof(PostgresRepositoryTestCollection))]
public class PostgresRepositoryTestCollection : ICollectionFixture<PostgresRepositoryTestDatabaseFixture>
{
    
}