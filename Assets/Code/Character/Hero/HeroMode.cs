using Code.Character.Hero.HeroInterfaces;
using Code.Debugers;
using UnityEngine;
using Zenject;

namespace Code.Character.Hero
{
    public class HeroMode: MonoBehaviour, IHeroMode
    {
        private IHero _hero;
        public Constants.HeroMode Mode { get; private set; }

        private void Awake()
        {
            _hero = GetComponent<IHero>();
        }

        public void SetDefaultMode()
        {
            Logg.ColorLog("HeroMode: Set Hand");
            Mode = Constants.HeroMode.Default;
            _hero.GunAttack.Disable();
            _hero.HandAttack.Enable();
            _hero.Animator.PlayEnterHandMode();
        }
        public void SetGunMode()
        {
            Logg.ColorLog("HeroMode: Set Gun");
            Mode = Constants.HeroMode.Default;
            _hero.GunAttack.Enable();
            _hero.HandAttack.Disable();
            _hero.Animator.PlayEnterGunMode();
        }
        
    }
}