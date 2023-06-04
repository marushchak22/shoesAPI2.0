using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Nazar2API.Models;
using shoesAPI.Clients;
using shoesAPI.Models;

namespace Nazar2API.Controllers;

public class HomeController : Controller
{
    private readonly ShoesClient _shoesClient;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, ShoesClient shoesClient)
    {
        _logger = logger;
        _shoesClient = shoesClient;
    }


    public async Task<IActionResult> Index()
    {
        var sneakersJson = await _shoesClient.GetShoesbyBrands();

        var sneakers = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SneakersModel>>(sneakersJson);

        var result = new List<SneakersModel>();

        foreach (var sneaker in sneakers)
        {
            var sneakersModel = new SneakersModel
            {
                shoeName = sneaker.shoeName,
                brand = sneaker.brand,
                colorway = sneaker.colorway,
                styleID = sneaker.styleID,
                thumbnail = sneaker.thumbnail,
                lowestResellPrice = new lowestResellPrice
                {
                    stockX = sneaker.lowestResellPrice?.stockX,
                    goat = sneaker.lowestResellPrice?.goat,
                    flightClub = sneaker.lowestResellPrice?.flightClub
                }

            };
            result.Add(sneakersModel);
        }

        return View(result);
    }



    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
