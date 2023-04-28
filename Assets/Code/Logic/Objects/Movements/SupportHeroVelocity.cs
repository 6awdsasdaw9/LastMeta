using Code.Character.Hero;
using UnityEngine;
using Zenject;

namespace Code.Logic.Objects
{
    public class SupportHeroVelocity: MonoBehaviour
    {
        private HeroMovement _hero;

        [Inject]
        private void Construct(HeroMovement heroMovement)
        {
            _hero = heroMovement;
        }
    }
}