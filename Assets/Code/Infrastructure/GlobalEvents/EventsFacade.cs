using System;
using Code.Logic.LanguageLocalization;

namespace Code.Infrastructure.GlobalEvents
{
    public class EventsFacade
    {
        public SceneEvents SceneEvents { get; } = new();
        public TimeEvents TimeEvents { get; } = new();
        public GameEvents GameEvents { get; } = new();
        public HudEvents HudEvents { get; } = new();
    }

    public class GameEvents
    {
        public void ChoiceLanguageEvent(Language language) => OnChoiceLanguage?.Invoke(language);
        public Action<Language> OnChoiceLanguage;
    }

    public class HudEvents
    {
        public void PressButtonLanguageEvent(Language language) => OnPressButtonLanguage?.Invoke(language);
        public Action<Language> OnPressButtonLanguage;
    }
}