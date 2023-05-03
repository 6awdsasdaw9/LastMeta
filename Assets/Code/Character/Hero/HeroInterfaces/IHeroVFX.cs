using UnityEngine;

namespace Code.Character.Hero
{
    public interface IHeroVFX
    {
        void PlayDeathVFX();
        void PlayDeathVFX(Vector3 vfxPosition);
    }
}