using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assets.Scripts.Core.Assets
{
    public class AssetsManager : IAssetsManager
    {
        Dictionary<string, AddressablePool<object>> _assets;


        public AssetsManager()
        {
            _assets = new Dictionary<string, AddressablePool<object>>();
        }

        public async Task<T> GetAsync<T>(string address) where T : class
        {
            if (!_assets.ContainsKey(address))
            {
                _assets[address] = new AddressablePool<object>(address);
            }
            return await _assets[address].GetAsync() as T;
        }
    }
}