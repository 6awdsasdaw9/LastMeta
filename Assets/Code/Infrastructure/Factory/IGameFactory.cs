
using UnityEngine;

namespace Code.Infrastructure.Factory
{
    public interface IGameFactory 
    {
        GameObject CreateHero(GameObject at);
        void CreateHud();
    }
}