using Code.UI;
using UnityEngine;
using UnityEngine.Rendering.UI;
using Zenject;

namespace Code.Logic.Interactive
{
    public class InteractiveImage : MonoBehaviour,IInteractive
    {
        [SerializeField] private Sprite _sprite;
        private Hud _hud;

        [Inject]
        private void Construct(Hud hud)
        {
            _hud = hud;
        }
        public void StartInteractive()
        {
                 
        }

        public void StopInteractive()
        {
          ;
        }
    }
}