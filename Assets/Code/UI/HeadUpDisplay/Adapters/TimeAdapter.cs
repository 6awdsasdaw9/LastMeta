using Code.Character.Common.CommonCharacterInterfaces;
using Code.Infrastructure.GlobalEvents;
using Code.Logic.Objects.Animations;
using Code.Services;
using Code.Services.EventsSubscribes;
using Code.Services.GameTime;
using Code.UI.HeadUpDisplay.Elements;
using UnityEngine;
using Zenject;

namespace Code.UI.HeadUpDisplay.Adapters
{
    public class TimeAdapter : ITickable, IEventsSubscriber, IDisabledComponent
    {
        private HudSlider _hudSliderTimeOfDay;
        private StartStopAnimation _handleAnimation;
        
        private TextPanel _dayNumber;
        private GameClock _gameClock;
        private EventsFacade _eventsFacade;
        
        private bool _isFollowToClock;

        [Inject]
        private void Construct(HudFacade hudFacade, GameClock gameClock, EventsFacade eventsFacade)
        {
            _hudSliderTimeOfDay = hudFacade.SliderTimeOfDay;
            _handleAnimation = hudFacade.HandleTimeOfDay;
            _dayNumber = hudFacade.DayPanel;
            _gameClock = gameClock;
            _eventsFacade = eventsFacade;
            SubscribeToEvents(true);
        }


        public void Tick()
        {
            if (_isFollowToClock)
            {
                _hudSliderTimeOfDay.SetValue(_gameClock.DayTimeNormalized);
            }
        }
        

        public void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _eventsFacade.SceneEvents.OnLoadScene += EnableComponent;
                _eventsFacade.TimeEvents.OnStartMorning += RefreshDayText;
                
                _eventsFacade.TimeEvents.OnStartMorning += ShowSunSliderHandle;
                _eventsFacade.TimeEvents.OnStartNight += ShowMoonHandle;
            }
            else
            {
                _eventsFacade.SceneEvents.OnLoadScene -= EnableComponent;
                _eventsFacade.TimeEvents.OnStartMorning -= RefreshDayText;
                
                _eventsFacade.TimeEvents.OnStartMorning -= ShowSunSliderHandle;
                _eventsFacade.TimeEvents.OnStartNight -= ShowMoonHandle;
            }
        }

        public void DisableComponent()
        {
            _isFollowToClock = false;
        }

        public void EnableComponent()
        {
            RefreshDayText();
            _isFollowToClock = true;
            if (_gameClock.IsNightTime)
            {
                _handleAnimation?.PlayStop();
            }
        }
        private void ShowSunSliderHandle()
        {
            _handleAnimation?.PlayStart();
        }

        private void ShowMoonHandle()
        {
            _handleAnimation?.PlayStop();
        }

        private void RefreshDayText()
        {
            _dayNumber.SetText(_gameClock.CurrentTime.Day.ToString());
        }
    }
}