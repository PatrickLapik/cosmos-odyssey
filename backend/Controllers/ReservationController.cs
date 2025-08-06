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

    [HttpPost("show")]
    public async Task<IActionResult> Get([FromBody] SeeReservationRequest request)
    {
        var reservations = await reservationService.GetByFirstAndLastName(request);
        return Ok(reservations);
    }
}