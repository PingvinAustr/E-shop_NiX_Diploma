using Basket.Server.Models;
using Basket.Server.Models.Dtos;
using Basket.Server.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
[Scope("openid")]
[Route(ComponentDefaults.DefaultRoute)]
public class BasketBffController : ControllerBase
{
    private readonly ILogger<BasketBffController> _logger;
    private readonly IBasketService _basketService;

    public BasketBffController(
        ILogger<BasketBffController> logger,
        IBasketService basketService)
    {
        _logger = logger;
        _basketService = basketService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> AddProduct([FromBody] string plainTextData)
    {
        try
        {
            Console.WriteLine(plainTextData);
            List<int> carIds = JsonConvert.DeserializeObject<List<int>>(plainTextData);
            List<CarDto> cars = new List<CarDto>();
            Console.WriteLine(carIds.Count);
            foreach (var item in carIds)
            {
               CarDto car = new CarDto();
               car = await GetCarInfo(item);
               cars.Add(car);
            }
            foreach (var car in cars)
            {
                Console.WriteLine("[" + car.CarName + "]");
            }

            return Ok(cars);
        }
        catch (Exception ex)
        {
            // Handle any errors and return a bad request response
            Console.WriteLine(ex.Message);
            return BadRequest();
        }

    }

    public async Task<CarDto> GetCarInfo(int id)
    {
        // Build the API endpoint URL with the provided ID
        string apiUrl = $"http://www.alevelwebsite.com:5000/api/v1/CatalogBff/GetById?id={id}";

        HttpClient _httpClient = new HttpClient();
        // Send an HTTP GET request to the API and retrieve the JSON response
        HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
        string jsonResponse = await response.Content.ReadAsStringAsync();

        // Deserialize the JSON response into a CarDto object
        dynamic responseData = JsonConvert.DeserializeObject(jsonResponse);
        dynamic carData = responseData.data[0];
        CarDto car = new CarDto
        {
            CarId = carData.carId,
            CarName = carData.carName,
            CarPromo = carData.carPromo,
            Price = carData.price,
            ImageFileName = carData.imageFileName,
            IsAvailable = carData.isAvailable,
            CarType = new TypeDto
            {
                TypeId = carData.carType.typeId,
                TypeName = carData.carType.typeName,
                TypeDescription = carData.carType.typeDescription
            },
            Manufacturer = new ManufacturerDto
            {
                ManufacturerId = carData.manufacturer.manufacturerId,
                ManufacturerName = carData.manufacturer.manufacturerName,
                ManufacturerCountry = carData.manufacturer.manufacturerCountry
            }
        };

        return car;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> CreateOrder(string plainTextData)
    {
        Console.WriteLine("PLAIN TEXT");
        Console.WriteLine("[" + plainTextData + "]");
        List <CarDto> carDtos = new List<CarDto>();
        carDtos = JsonConvert.DeserializeObject<List<CarDto>>(plainTextData);
        Console.WriteLine("COUNT" + carDtos.Count);


        CreateOrderRequest order = new CreateOrderRequest();
        int totalSum = 0;
        int orderCount = 0;
        order.DateTime = DateTime.Now;
        order.OrderCars = new List<CarDto>();
        Console.WriteLine("JSON:");
        foreach (CarDto carDto in carDtos)
        {
            order.OrderCars.Add(carDto);
            totalSum += carDto.Price;
            orderCount++;
            Console.WriteLine(carDto.CarName);
        }

        order.TotalSum = totalSum;
        order.OrderCount = orderCount;

        Random random = new Random();
        order.OrderId = random.Next(100000,999999);
  
        return Ok(order);
    }
}