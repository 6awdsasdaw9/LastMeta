using Code.Infrastructure.Services;
using UnityEngine;

namespace Code.Infrastructure.Facroty
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject at);
        void CreateHud();
    }
}