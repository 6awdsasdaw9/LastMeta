using System;
using Code.UI.Actors;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    [RequireComponent(typeof(HudAdapter))]
    public class Hud : MonoBehaviour
    {
        public Image InteractiveImage; 
        public HpBar HeroHpBar;

        public Action OnUIWindowShown;
        public Action OnUIWindowHidden;
        
        
        public void ShowInteractiveImage(Sprite sprite)
        {
            InteractiveImage.sprite = sprite;
            InteractiveImage.gameObject.SetActive(true);
            OnUIWindowHidden?.Invoke();
        }

        public void HideInteractiveImage()
        {
            InteractiveImage.gameObject.SetActive(false);
            OnUIWindowHidden?.Invoke();
        }
    }
}