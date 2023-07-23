using System;
using Code.Infrastructure.GlobalEvents;
using Code.Services.CurrencyServices;
using Code.Services.SaveServices;
using Code.UI.HeadUpDisplay;

namespace Code.Logic.Objects.Items.Handlers
{
    public class MoneyHandler: ISavedData
    {
        private readonly MoneyStorage _moneyStorage;
        private readonly HudFacade _hudFacade;
        private readonly EventsFacade _eventsFacade;

        public MoneyHandler(MoneyStorage moneyStorage, HudFacade hudFacade,SavedDataStorage savedDataStorage, EventsFacade eventsFacade)
        {
            _moneyStorage = moneyStorage;
            _hudFacade = hudFacade;
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
            _hudFacade.MoneyPanel.SetText(currentMoney.ToString());
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