using Code.UI;
using UnityEngine;
using Zenject;

namespace Code.Logic.Interactive.InteractiveObjects.Laptop
{
    public class InteractiveObjectLaptop : MonoBehaviour,IInteractive
    {
        private HUD _hud;
        

        [Inject]
        private void Construct(HUD hud)
        {
            _hud = hud;
        }
        public void StartInteractive()
        {
            
        }

        public void StopInteractive()
        {
            
        }
    }
}