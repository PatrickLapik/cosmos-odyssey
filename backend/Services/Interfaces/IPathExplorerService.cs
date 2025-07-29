using CosmosOdyssey.Models;

namespace CosmosOdyssey.Services;

public interface IPathExplorerService
{
    public List<List<CompanyRoute>> FindAllValidPaths(Guid from, Guid to, DateTime start); 
}