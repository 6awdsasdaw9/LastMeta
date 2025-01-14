using System;
using Code.Debugers;
using Code.Logic.Items;
using Code.Services.LanguageLocalization;
using Code.UI.HeadUpDisplay.Windows;

namespace Code.Infrastructure.GlobalEvents
{
    public class EventsFacade
    {
        public SceneEvents SceneEvents { get; } = new();
        public TimeEvents TimeEvents { get; } = new();
        public GameEvents GameEvents { get; } = new();
        public HudEvents HudEvents { get; } = new();
        public ItemEvents ItemEvents { get; } = new();
        public HeroEvents HeroEvents { get; } = new();
    }

    public class HeroEvents
    {
        public void HeroWoundEvent()
        {
            OnHeroWounded?.Invoke();
        }
        public Action OnHeroWounded;

        public void HeroHealthyEvent()
        {
            OnHeroHealth?.Invoke();
        }
        public Action OnHeroHealth;

    }

    public class ItemEvents
    {
        public void PickUpItemEvent(ItemData item) => OnPickUpItem?.Invoke(item);
        public Action<ItemData> OnPickUpItem;
    }

    public class GameEvents
    {
        public void ChoiceLanguageEvent(Language language) => OnChoiceLanguage?.Invoke(language);
        public Action<Language> OnChoiceLanguage;

        public void PauseEvent(bool isPause) => OnPause?.Invoke(isPause);
        public Action<bool> OnPause;

    }

    public class HudEvents
    {
        public void PressButtonLanguageEvent(Language language) => OnPressButtonLanguage?.Invoke(language);
        public Action<Language> OnPressButtonLanguage;

        public void WindowShownEvent(IWindow window) => OnWindowShown?.Invoke(window);
        public Action<IWindow> OnWindowShown;
        public void WindowHiddenEvent(IWindow window) => OnWindowHidden?.Invoke(window);
        public Action<IWindow> OnWindowHidden;


        public void OpenFirstWindowEvent() => OnOpenFirstWindow?.Invoke();
        public Action OnOpenFirstWindow;

        public void CloseLastWindowEvent() => OnCloseLastWindow?.Invoke();
        public Action OnCloseLastWindow;

    }
}