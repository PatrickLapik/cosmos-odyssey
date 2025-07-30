using CosmosOdyssey.Dtos.Response;
using CosmosOdyssey.Models;

namespace CosmosOdyssey.Services.Interfaces;

public interface ICompanyService
{
    public Task Save(Company company);
    public Task<Company?> Get(Guid id);
    public Task<List<CompanyResponse>> GetAll();
}