using Code.Character.Common.CommonCharacterInterfaces;
using Code.Infrastructure.GlobalEvents;
using Code.Logic.DayOfTime;
using Code.PresentationModel;
using Code.PresentationModel.Buttons;
using Code.PresentationModel.HeadUpDisplay;
using Code.PresentationModel.HudElements;
using Code.Services;
using UnityEngine;
using Zenject;

namespace Code.Logic.Adaptors
{
    public class ClockAdapter : MonoBehaviour, IEventSubscriber, IDisabledComponent
    {
        private HudSlider _hudSliderTimeOfDay;
        private StartStopAnimation _handleAnimation;
        
        private TextPanel _dayNumber;
        private GameClock _gameClock;
        private EventsFacade _eventsFacade;
        
        private bool _isFollowToClock;

        [Inject]
        private void Construct(Hud hud, GameClock gameClock, EventsFacade eventsFacade)
        {
            _hudSliderTimeOfDay = hud.SliderTimeOfDay;
            _handleAnimation = hud.HandleTimeOfDay;
            _dayNumber = hud.DayPanel;
            _gameClock = gameClock;
            _eventsFacade = eventsFacade;
        }

        private void OnEnable()
        {
            SubscribeToEvent(true);
            RefreshDayText();
        }

        private void LateUpdate()
        {
            if (_isFollowToClock)
            {
                _hudSliderTimeOfDay.SetValue(_gameClock.DayTimeNormalized);
            }
        }

        private void OnDisable()
        {
            SubscribeToEvent(false);
        }

        public void SubscribeToEvent(bool flag)
        {
            if (flag)
            {
                _eventsFacade.SceneEvents.OnLoadScene += Enable;
                _eventsFacade.TimeEvents.OnStartMorning += RefreshDayText;
                
                _eventsFacade.TimeEvents.OnStartMorning += ShowSunSliderHandle;
                _eventsFacade.TimeEvents.OnStartNight += ShowMoonHandle;
            }
            else
            {
                _eventsFacade.SceneEvents.OnLoadScene -= Enable;
                _eventsFacade.TimeEvents.OnStartMorning -= RefreshDayText;
                
                _eventsFacade.TimeEvents.OnStartMorning -= ShowSunSliderHandle;
                _eventsFacade.TimeEvents.OnStartNight -= ShowMoonHandle;
            }
        }

        private void ShowSunSliderHandle()
        {
            _handleAnimation.PlayStart();
        }

        private void ShowMoonHandle()
        {
            _handleAnimation.PlayStop();
        }

        private void RefreshDayText()
        {
            _dayNumber.SetText(_gameClock.CurrentTime.Day.ToString());
        }


        public void Disable()
        {
            _isFollowToClock = false;
        }

        public void Enable()
        {
            _isFollowToClock = true;
            if(_gameClock.IsNightTime)_handleAnimation.PlayStop();
        }
    }
}