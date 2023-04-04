using Code.UI;
using UnityEngine;

namespace Code.Logic.Interactive.InteractiveObjects.Laptop
{
    public class WindowLaptopMessenger : MonoBehaviour, IWindow
    {
        public void ShowOrHide()
        {
            if (gameObject.activeSelf)
                HideWindow();
            else
                ShowWindow();
        }

        public void ShowWindow() =>
            gameObject.SetActive(true);

        public void HideWindow() =>
            gameObject.SetActive(false);
    }
}