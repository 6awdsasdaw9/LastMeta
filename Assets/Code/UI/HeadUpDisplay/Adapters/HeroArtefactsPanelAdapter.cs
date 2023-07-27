using System.Linq;
using Code.Character.Hero.HeroInterfaces;
using Code.Infrastructure.GlobalEvents;
using Code.Logic.Objects.Items;
using Code.Services;
using Code.Services.SaveServices;
using Zenject;

namespace Code.UI.HeadUpDisplay.Adapters
{
    public class HeroArtefactsPanelAdapter: IEventSubscriber, ISavedDataReader
    {
        private readonly IHero _hero;
        private readonly HudFacade _hudFacade;
        private readonly EventsFacade _eventsFacade;

        public HeroArtefactsPanelAdapter(DiContainer container)
        {
            _hero = container.Resolve<IHero>();
            _hudFacade = container.Resolve<HudFacade>();
            _eventsFacade = container.Resolve<EventsFacade>();
            
            container.Resolve<SavedDataStorage>().Add(this);
            container.Resolve<EventSubsribersStorage>().Add(this);
            
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
            var icons = _hudFacade.Menu.Window.Hero.ArtifactsPanel.ArtifactIcons;
            
            var ability = _hero.Ability.GetAbility(item.Type);
            var artefactIcon = icons.FirstOrDefault(i => i.Type == item.Type);
            
            if(artefactIcon == null || ability == null) return;

            artefactIcon.DescriptionPanel.SetLevel(ability.Level.ToString());
        }

        public void LoadData(SavedData savedData)
        {
            
        }
    }
}