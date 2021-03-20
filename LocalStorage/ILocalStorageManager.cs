using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.Clients.LocalStorage
{
    /// <summary>
    /// This abstraction exists so that we can inject a different local storage provider into Blazor, iOS, Android and other applications.
    /// </summary>
    public interface ILocalStorageManager
    {
        void Clear();
        ValueTask ClearAsync();
        bool ContainKey(string key);
        ValueTask<bool> ContainKeyAsync(string key);
        T GetItem<T>(string key);
        string GetItemAsString(string key);
        ValueTask<string> GetItemAsStringAsync(string key);
        ValueTask<T> GetItemAsync<T>(string key);
        string Key(int index);
        ValueTask<string> KeyAsync(int index);
        int Length();
        ValueTask<int> LengthAsync();
        void RemoveItem(string key);
        ValueTask RemoveItemAsync(string key);
        void SetItem<T>(string key, T data);
        ValueTask SetItemAsync<T>(string key, T data);
    }
}
