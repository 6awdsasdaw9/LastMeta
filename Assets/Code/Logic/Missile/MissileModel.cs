using UnityEngine;

namespace Code.Logic.Missile
{
    public class MissileModel : MonoBehaviour
    {
        [SerializeField] private Missile _missile;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void SetRandomMissileSprite()
        {
            var sprites = _missile.ShootingParams.MissileSprites;
            _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        }
    }
}