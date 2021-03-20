using System;
using System.Threading.Tasks;

namespace Energetic.Clients.Cookies
{
    /// <summary>
    /// This abstraction exists so that we can inject a different cookie provider into Blazor, iOS, Android and other applications.
    /// </summary>
    public interface ICookieManager
    {
        ValueTask ClearAsync();

        ValueTask<bool> ContainsKeyAsync(string key);

        ValueTask<string> GetItemAsStringAsync(string key);

        ValueTask<T> GetItemAsync<T>(string key);

        ValueTask RemoveItemAsync(string key);

        ValueTask SetItemAsync<T>(string key, T data, int expiryDays);

        ValueTask SetItemAsync<T>(string key, T data, DateTimeOffset? expires = null);
    }
}