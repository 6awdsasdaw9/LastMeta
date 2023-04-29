using Code.Character.Hero;
using FMODUnity;
using UnityEngine;
using Zenject;

namespace Code.Audio
{
    [RequireComponent(typeof(StudioListener))]
    public class CameraAudioListener: MonoBehaviour
    {
        [SerializeField] private StudioListener _listener;
        
        [Inject]
        private void Construct(IHero hero)
        {
            _listener.attenuationObject = hero.Transform.gameObject;
        }
    }
}