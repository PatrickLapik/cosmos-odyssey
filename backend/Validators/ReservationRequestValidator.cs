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
            .NotEmpty().WithMessage("CompanyRouteIds are required")
            .MustAsync(AllCompanyRoutesExist)
            .WithMessage("One or more RouteIds dont exist");
        
        RuleFor(rr => rr.FirstName).NotEmpty().WithMessage("FirstName is required");
        RuleFor(rr => rr.LastName).NotEmpty().WithMessage("LastName is required");
    } 
    
    private async Task<bool> AllCompanyRoutesExist(List<Guid> companyRouteIds, CancellationToken cancellationToken)
    {
        var existingIds = await _context.CompanyRoutes
            .Where(cr => companyRouteIds.Contains(cr.Id))
            .Select(cr => cr.Id)
            .ToListAsync(cancellationToken);

        return companyRouteIds.All(id => existingIds.Contains(id));
    }    
}