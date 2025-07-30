using CosmosOdyssey.Data;
using CosmosOdyssey.Dtos;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CosmosOdyssey.Validators;

public class ReservationRequestValidator : AbstractValidator<ReservationRequest>
{
    private readonly AppDbContext _context;

    public ReservationRequestValidator(AppDbContext context)
    {
        _context = context;

        RuleFor(rr => rr.CompanyRouteIds)
            .NotEmpty()
            .MustAsync(AllCompanyRoutesExist)
            .WithMessage("One or more RouteIds dont exist")
            .MustAsync(CompanyRoutesAreStillValid)
            .WithMessage("One or more RouteIds are not available for booking");

        RuleFor(rr => rr.FirstName).NotEmpty();
        RuleFor(rr => rr.LastName).NotEmpty();

        RuleFor(rr => rr)
            .MustAsync(NoDuplicates)
            .WithMessage("This user has already booked one or more of the selected routes");
    }

    private async Task<bool> NoDuplicates(ReservationRequest request, CancellationToken cancellationToken)
    {
        return !await _context.Reservations
            .Where(r => r.FirstName == request.FirstName && r.LastName == request.LastName)
            .AnyAsync(r =>
                r.CompanyRoutes.Any(cr => request.CompanyRouteIds.Contains(cr.Id)), cancellationToken);
    }

    private async Task<bool> AllCompanyRoutesExist(List<Guid> companyRouteIds, CancellationToken cancellationToken)
    {
        var existingCount = await _context.CompanyRoutes
            .CountAsync(r => companyRouteIds.Contains(r.Id), cancellationToken);

        return existingCount == companyRouteIds.Count;
    }

    private async Task<bool> CompanyRoutesAreStillValid(List<Guid> companyRouteIds, CancellationToken cancellationToken)
    {
        var validExistingRecordsCount = await _context.CompanyRoutes
            .Include(cr => cr.TravelPrice)
            .Where(cr => companyRouteIds.Contains(cr.Id) && cr.TravelPrice.ValidUntil >= DateTime.Now)
            .CountAsync(cancellationToken);

        return validExistingRecordsCount == companyRouteIds.Count;
    }
}