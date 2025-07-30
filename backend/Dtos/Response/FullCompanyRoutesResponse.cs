namespace CosmosOdyssey.Dtos;

public class FullCompanyRoutesResponse
{
    public List<CompanyRouteResponse> CompanyRouteResponses { get; set; }
    public double TotalPrice { get; set; }
    public long TotalTravelMinutes { get; set; }
    public long TotalDistance { get; set; }
}