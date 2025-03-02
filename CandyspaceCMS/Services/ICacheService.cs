namespace CandyspaceCMS.Services
{
    public interface ICacheService
    {
        void Add<T>(string key, T value, TimeSpan expiration);
        T? Get<T>(string key);
        bool Contains(string key);
        void Remove(string key);
    }
}
