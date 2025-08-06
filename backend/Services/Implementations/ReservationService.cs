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
            .OrderBy(cr => cr.TravelStart)
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

    public async Task<List<ReservationResponse>> GetByFirstAndLastName(SeeReservationRequest request)
    {
        var reservations = await Context.Reservations
            .Include(r => r.CompanyRoutes)
                .ThenInclude(cr => cr.Company) 
            .Include(r => r.CompanyRoutes)
                .ThenInclude(cr => cr.Route) 
                    .ThenInclude(r => r!.FromDestination)
            .Include(r => r.CompanyRoutes)
                .ThenInclude(cr => cr.Route) 
                    .ThenInclude(r => r!.ToDestination)
            .Include(r => r.CompanyRoutes.OrderBy(cr => cr.TravelStart))
            .Where(cr => cr.FirstName.ToLower() == request.FirstName.ToLower() && cr.LastName.ToLower() == request.LastName.ToLower())
            .ToListAsync();

        return mapper.Map<List<Reservation>, List<ReservationResponse>>(reservations);
    }
}