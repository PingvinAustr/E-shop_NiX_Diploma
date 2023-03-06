using Basket.Host.Models;

namespace Basket.Host.Services.Interfaces;

public interface IBasketService
{
    Task AddProduct(string userId, CarAddRequest data);
    Task<TestGetResponse> TestGet(string userId);
}