using AutoMapper;
using CosmosOdyssey.Data;
using CosmosOdyssey.Dtos.Request;
using CosmosOdyssey.Dtos.Response;
using CosmosOdyssey.Models;
using CosmosOdyssey.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CosmosOdyssey.Services.Implementations;

public class ReservationService(AppDbContext context, IMapper mapper) : BaseService(context), IReservationService
{
    public async Task Save(ReservationRequest reservationRequest)
    {
        var companyRoutes = await Context.CompanyRoutes
            .Where(cr => reservationRequest.CompanyRouteIds.Contains(cr.Id))
            .ToListAsync();

        var newReservation = new Reservation
        {
            FirstName = reservationRequest.FirstName,
            LastName = reservationRequest.LastName,
            CompanyRoutes = companyRoutes,
            TotalPrice = companyRoutes.Sum(cr => cr.Price),
            TotalTravelMinutes = (companyRoutes.Last().TravelEnd - companyRoutes.First().TravelStart).TotalMinutes
        };

        await Context.Reservations.AddAsync(newReservation);
        await Context.SaveChangesAsync();
    }

    public async Task<List<ReservationResponse>> GetByFirstAndLastName(string firstName, string lastName)
    {
        var reservations = await Context.Reservations
            .Include(r => r.CompanyRoutes)
            .ThenInclude(r => r.Company)
            .Include(r => r.CompanyRoutes)
            .ThenInclude(r => r.Route)
            .ThenInclude(cr => cr.ToDestination)
            .Include(r => r.CompanyRoutes)
            .ThenInclude(r => r.Route)
            .ThenInclude(cr => cr.FromDestination)
            .Where(cr => cr.FirstName.ToLower() == firstName.ToLower() && cr.LastName.ToLower() == lastName.ToLower())
            .ToListAsync();

        return mapper.Map<List<Reservation>, List<ReservationResponse>>(reservations);
    }
}