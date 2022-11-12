using UnityEngine;

namespace Code.Infrastructure.AssetManagement
{
    public class Assets : IAssets
    {
        public GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }

        public GameObject Instantiate(string path)
        {
            var heroPrefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(heroPrefab);
        }
    }
}