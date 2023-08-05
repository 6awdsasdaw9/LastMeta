using Code.Logic.Common;
using UnityEngine;

namespace Code.Character.Hero.HeroInterfaces
{
    public interface IHeroVFX
    {
        SpriteVFX SpriteVFX { get; }
        void PlayDeathVFX();
        void PlayDeathVFX(Vector3 vfxPosition);
    }
}