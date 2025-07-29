using CosmosOdyssey.Data;
using CosmosOdyssey.Dtos;
using CosmosOdyssey.Models;
using CosmosOdyssey.Services;
using CosmosOdyssey.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Pagination.EntityFrameworkCore.Extensions;
using Route = CosmosOdyssey.Models.Route;

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
    public async Task<IActionResult> SaveReservation([FromBody] ReservationRequest request)
    {
        await _reservationService.Save(request);
        return Ok();
    }

    [HttpGet("{firstName}/{lastName}")]
    public async Task<IActionResult> GetReservation(string firstName, string lastName)
    {
        var reservations = await _reservationService.GetByFirstAndLastName(firstName, lastName);
        return Ok(reservations);
    }
}