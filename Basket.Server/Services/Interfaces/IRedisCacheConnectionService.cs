using StackExchange.Redis;

namespace Basket.Server.Services.Interfaces
{
    public interface IRedisCacheConnectionService
    {
        public IConnectionMultiplexer Connection { get; }
    }
}