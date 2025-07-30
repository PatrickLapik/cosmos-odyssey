using CosmosOdyssey.Dtos;
using CosmosOdyssey.Dtos.Request;
using CosmosOdyssey.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CosmosOdyssey.Controllers;

[ApiController]
[Route("reservations")]
public class ReservationController(IReservationService reservationService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Save([FromBody] ReservationRequest request)
    {
        await reservationService.Save(request);
        return Ok();
    }

    [HttpGet("{firstName}/{lastName}")]
    public async Task<IActionResult> Get(string firstName, string lastName)
    {
        var reservations = await reservationService.GetByFirstAndLastName(firstName, lastName);
        return Ok(reservations);
    }
}