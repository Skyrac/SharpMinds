namespace SharpMinds.Integration.Tests;

[CollectionDefinition(nameof(PostgresTestCollection))]
public class PostgresTestCollection : ICollectionFixture<PostgresTestDatabaseFixture>
{
    
}