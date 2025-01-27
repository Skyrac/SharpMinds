namespace SharpMinds.SoftwarePattern.Repository;

public class Repository<TEntity>(RepositoryDbContext context) 
    : GenericRepository<TEntity, RepositoryDbContext>(context) where TEntity : class
{
    
}