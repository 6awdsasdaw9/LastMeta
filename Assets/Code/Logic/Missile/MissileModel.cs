using UnityEngine;

namespace Code.Logic.Missile
{
    public class MissileModel : MonoBehaviour
    {
        [SerializeField] private HeroMissile heroMissile;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void SetRandomMissileSprite()
        {
            var sprites = heroMissile.ShootingParams.MissileSprites;
            _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        }
    }
}