using CosmosOdyssey.Dtos;
using CosmosOdyssey.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CosmosOdyssey.Controllers;

[ApiController]
[Route("reservations")]
public class ReservationController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpPost]
    public async Task<IActionResult> Save([FromBody] ReservationRequest request)
    {
        await _reservationService.Save(request);
        return Ok();
    }

    [HttpGet("{firstName}/{lastName}")]
    public async Task<IActionResult> Get(string firstName, string lastName)
    {
        var reservations = await _reservationService.GetByFirstAndLastName(firstName, lastName);
        return Ok(reservations);
    }
}