using System.Threading.Tasks;

namespace AutoMarket.Api.Infrastructures.Cache
{
    public interface ICacheService
    {
        Task<T> Get<T>(string key);
        Task Add(string key, object data);
        Task Remove(string key);
        void Clear();
        Task<bool> Any(string key);
    }
}
