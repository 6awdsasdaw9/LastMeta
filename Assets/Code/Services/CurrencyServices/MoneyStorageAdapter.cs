using System;
using Code.Infrastructure.GlobalEvents;
using Code.Logic.Artifacts;
using Code.PresentationModel.HeadUpDisplay;
using Code.Services.SaveServices;

namespace Code.Services.CurrencyServices
{
    public class MoneyStorageAdapter: ISavedData
    {
        private readonly MoneyStorage _moneyStorage;
        private readonly Hud _hud;
        private readonly EventsFacade _eventsFacade;

        public MoneyStorageAdapter(MoneyStorage moneyStorage, Hud hud,SavedDataStorage savedDataStorage, EventsFacade eventsFacade)
        {
            _moneyStorage = moneyStorage;
            _hud = hud;
            _eventsFacade = eventsFacade;

            savedDataStorage.Add(this);
            SubscribeToEvent();
        }
        
        private void SubscribeToEvent()
        {
            _eventsFacade.ItemEvents.OnPickUpItem += OnPickUpItem;
            _moneyStorage.OnChangeValue += OnChangeValue;
        }

        private void OnPickUpItem(ItemData itemData)
        {
            if(itemData.Type != ItemType.Money) return;
            
            _moneyStorage.Add((int)Math.Round(itemData.Value));
        }

        private void OnChangeValue(int currentMoney)
        {
            _hud.MoneyPanel.SetText(currentMoney.ToString());
        }

        public void LoadData(SavedData savedData)
        {
            _moneyStorage.Set(savedData.Money);
        }

        public void SaveData(SavedData savedData)
        {
            savedData.Money = _moneyStorage.Current;
        }
    }
}