using System.Linq;
using Code.Character.Hero.Abilities;
using Code.Character.Hero.HeroInterfaces;
using Code.Infrastructure.GlobalEvents;
using Code.Logic.Objects.Items;
using Code.Services;
using Code.Services.SaveServices;
using Code.UI.HeadUpDisplay.Windows.HudWindows.MenuWindowElements.Hero.ArtifactsElements;
using Zenject;

namespace Code.UI.HeadUpDisplay.Adapters
{
    public class HeroArtefactsPanelAdapter: IEventsSubscriber, ISavedDataReader
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
            
            SubscribeToEvents(true);
        }


        public void SubscribeToEvents(bool flag)
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
            RefreshIcon(item.Type);
        }

        private void RefreshIcon(ItemType itemType)
        {
            GetComponents(itemType, out ArtifactIcon artefactIcon, out Ability ability);
            if (artefactIcon == null || ability == null) return;
            if (ability.Level > 0)
            {
                artefactIcon.EnableIcon();
                artefactIcon.DescriptionPanel?.SetLevel((ability.Level + 1).ToString());
            }
            else
            {
                artefactIcon.DisableIcon();
            }
        }

        private void GetComponents(ItemType itemType, out ArtifactIcon artifactIcon, out Ability ability)
        {
            var icons = _hudFacade.Menu.Window.Hero.ArtifactsPanel.ArtifactIcons;
            artifactIcon =  icons.FirstOrDefault(i => i.Type == itemType);
            ability = _hero.Ability.GetAbility(itemType);
        }
        public void LoadData(SavedData savedData)
        {
            RefreshIcon(ItemType.Glove);
            RefreshIcon(ItemType.Gun);
            RefreshIcon(ItemType.RightSock);
            RefreshIcon(ItemType.LeftSock);
            RefreshIcon(ItemType.Substance);
        }
    }
}