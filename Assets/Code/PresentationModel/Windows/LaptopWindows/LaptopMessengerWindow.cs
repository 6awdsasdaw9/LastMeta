using System;
using UnityEngine;

namespace Code.PresentationModel.Windows.LaptopWindows
{
    public class LaptopMessengerWindow : MonoBehaviour, IWindow
    {
        [SerializeField] private GameObject _body;
        public void ShowOrHide()
        {
            if (gameObject.activeSelf)
                HideWindow();
            else
                ShowWindow();
        }

        public void ShowWindow(Action WindowShowed = null)
        {
            WindowShowed?.Invoke();
            _body.SetActive(true);
        }

        public void HideWindow(Action WindowHidden = null)
        {
            WindowHidden?.Invoke();
            _body.SetActive(false);
        }
    }
}