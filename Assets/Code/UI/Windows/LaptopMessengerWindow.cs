using UnityEngine;

namespace Code.UI.Windows
{
    public class LaptopMessengerWindow : MonoBehaviour, IWindow
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