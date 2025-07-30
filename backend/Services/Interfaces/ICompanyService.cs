using CosmosOdyssey.Dtos;
using CosmosOdyssey.Models;

namespace CosmosOdyssey.Services;

public interface ICompanyService
{
    public Task Save(Company company);
    public Task<Company?> Get(Guid id);
    public Task<List<CompanyResponse>> GetAll();
}