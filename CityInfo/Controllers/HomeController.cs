using Microsoft.AspNetCore.Mvc;
using CityInfo.Services;
using Servics.LocationInformationServic;
using Domen.Entities;

namespace CityInfo.Controllers;
[Route("Search")]
[ApiController]
public class HomeController : ControllerBase
{
    private readonly ILocationInfoServic _locationInfoServic;
    public HomeController(ILocationInfoServic locationInfoServic)
    {
        this._locationInfoServic = locationInfoServic;
    }
    [HttpGet]
    public async Task<ActionResult<LocationInformation>> Get(string City)
    {
        var city = await _locationInfoServic.GetLocationInformationAsync(City);

        return Ok(city);
    }
}
