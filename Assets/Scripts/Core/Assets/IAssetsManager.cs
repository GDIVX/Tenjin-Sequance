using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Core.Assets
{
    public interface IAssetsManager
    {
        Task<T> GetAsync<T>(string address) where T : class;
    }
}