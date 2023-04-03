using Code.UI.Interfaces;
using UnityEngine;

namespace Code.Logic.Interactive.InteractiveObjects.Laptop
{
    public class LaptopMessenger: MonoBehaviour, IHUDWindow
    {
        public bool ActiveSelf => gameObject.activeSelf;
        public void ShowOrHide()
        {
            if(ActiveSelf)
                Hide();
            else
                Show();
        }

        public void Show() => 
            gameObject.SetActive(true);

        public void Hide() => 
            gameObject.SetActive(false);

    }
}