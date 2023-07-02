using Code.Character.Hero.HeroInterfaces;
using FMODUnity;
using UnityEngine;
using Zenject;

namespace Code.Logic.CameraLogic
{
    [RequireComponent(typeof(StudioListener))]
    public class CameraAudioListener: MonoBehaviour
    {
        [SerializeField] private StudioListener _listener;
        
        [Inject]
        private void Construct(IHero hero)
        {
        }
    }
}