using CosmosOdyssey.Dtos;
using CosmosOdyssey.Dtos.Request;
using CosmosOdyssey.Dtos.Response;

namespace CosmosOdyssey.Services.Interfaces;

public interface IReservationService
{
    public Task Save(ReservationRequest reservationRequest);
    public Task<List<ReservationResponse>> GetByFirstAndLastName(SeeReservationRequest request);
}