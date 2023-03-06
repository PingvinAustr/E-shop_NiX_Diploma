using Basket.Server.Models;

namespace Basket.Server.Services.Interfaces;

public interface IBasketService
{
    Task AddProduct(string userId, CarAddRequest data);
    Task<TestGetResponse> TestGet(string userId);
}