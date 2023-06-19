using System.Collections.Generic;
using Code.Data.Configs;
using Code.Infrastructure.GlobalEvents;
using Code.Services.SaveServices;

namespace Code.Logic.LanguageLocalization
{
    public class LocalizationController: ISavedData
    {

        private readonly EventsFacade _eventsFacade;
        
        private Language _currentLanguage;
        private TextConfig _currentTextConfig;

        public LocalizationController(
            EventsFacade eventsFacade, 
            SavedDataStorage savedDataStorage)
        {

            _eventsFacade = eventsFacade;
            _eventsFacade.HudEvents.OnPressButtonLanguage += OnPressButtonLanguage;
            savedDataStorage.Add(this);
        }

        private void OnPressButtonLanguage(Language obj)
        {
            _currentLanguage = obj;

            _eventsFacade.GameEvents.ChoiceLanguageEvent(_currentLanguage);
        }



        public void LoadData(SavedData savedData)
        {
            _currentLanguage = (Language)savedData.Language;
            _eventsFacade.GameEvents.ChoiceLanguageEvent(_currentLanguage);
        }

        public void SaveData(SavedData savedData)
        {
            savedData.Language = (int)_currentLanguage;
        }
    }

    public enum Language
    {
        Eng,
        Rus
    }
}