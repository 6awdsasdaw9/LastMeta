using System.Linq;
using Code.Character.Hero.HeroInterfaces;
using Code.Debugers;
using Code.Infrastructure.GlobalEvents;
using Code.Logic.Objects.Items;
using Code.Services;
using Code.Services.SaveServices;

namespace Code.UI.HeadUpDisplay.Adapters
{
    public class HeroArtefactsPanelAdapter: IEventSubscriber, ISavedDataReader
    {
        private readonly IHero _hero;
        private readonly HudFacade _hudFacade;
        private readonly EventsFacade _eventsFacade;

        public HeroArtefactsPanelAdapter(
            IHero hero, 
            HudFacade hudFacade, 
            EventsFacade eventsFacade, 
            SavedDataStorage savedDataStorage)
        {
            _hero = hero;
            _hudFacade = hudFacade;
            _eventsFacade = eventsFacade;
            savedDataStorage.Add(this);
            SubscribeToEvent(true);
        }


        public void SubscribeToEvent(bool flag)
        {
            if (flag)
            {
                _eventsFacade.ItemEvents.OnPickUpItem += OnPickUpItem;
            }
            else
            {
                _eventsFacade.ItemEvents.OnPickUpItem -= OnPickUpItem;
            }
        }

        private void OnPickUpItem(ItemData item)
        {
            Logg.ColorLog($"HeroArtefactsPanelAdapter: OnPickUpItem {item.Type}");
            var icons = _hudFacade.Menu.Window.Hero.ArtifactsPanel.ArtifactIcons;
            
            var ability = _hero.Ability.GetAbility(item.Type);
            var artefactIcon = icons.FirstOrDefault(i => i.Type == item.Type);
            Logg.ColorLog($"HeroArtefactsPanelAdapter: OnPickUpItem: icon == null {artefactIcon == null}  ability == null {ability == null}");
            
            if(artefactIcon == null || ability == null) return;

            Logg.ColorLog($"HeroArtefactsPanelAdapter: OnPickUpItem: level {ability.Level}.end");
            
            artefactIcon.DescriptionPanel.SetLevel(ability.Level.ToString());
        }

        public void LoadData(SavedData savedData)
        {
            
        }
    }
}