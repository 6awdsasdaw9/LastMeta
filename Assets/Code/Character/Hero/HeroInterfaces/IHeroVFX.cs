using UnityEngine;

namespace Code.Character.Hero.HeroInterfaces
{
    public interface IHeroVFX
    {
        void PlayDeathVFX();
        void PlayDeathVFX(Vector3 vfxPosition);
    }
}