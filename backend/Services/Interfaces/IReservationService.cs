using CosmosOdyssey.Dtos;

namespace CosmosOdyssey.Services.Interfaces;

public interface IReservationService
{
    public Task Save(ReservationRequest reservationRequest);
    public Task<List<ReservationResponse>> GetByFirstAndLastName(string firstName, string lastName);
}