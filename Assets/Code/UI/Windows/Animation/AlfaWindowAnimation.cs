using System.Collections;
using Code.Data.Configs;
using UnityEngine;
using Zenject;

namespace Code.UI.Windows
{
    public class AlfaWindowAnimation : WindowAnimation
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private  float _timeToHide;
        private  float _timeToShow;

        private const int CYCLE_STEPS = 100;
        private const float ONE_STEP = 1 / (float)CYCLE_STEPS;

        
        [Inject]
        private void Construct( GameSettings gameSettings)
        {

            _timeToHide = gameSettings.InteractiveObjectTimeToHide;
            _timeToShow = gameSettings.InteractiveObjectTimeToShow;
        }
        
        public override void PlayShow()
        {
            IsPlay = true;
        }

        public override void PlayHide()
        {
            
            IsPlay = true;
        }
        
        private IEnumerator ShowCoroutine()
        {
            IsPlay = true;
            
            var speed = ONE_STEP / (1 / _timeToShow);
            for (int i = 0; i < CYCLE_STEPS; i++)
            {
                _canvasGroup.alpha += ONE_STEP;
                yield return new WaitForSeconds(speed);
            }

            IsPlay = false;
        }
    }
}