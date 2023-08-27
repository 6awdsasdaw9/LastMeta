using System;
using UnityEngine;

namespace Code.Logic.Graphics
{
    [Serializable]
    public class SpriteFlipper
    {
        [SerializeField] private SpriteRenderer _sprite;

        public void Flip()
        {
            if(_sprite == null) return;
            _sprite.flipX = !_sprite.flipX;
        }

        public void Flip(bool isLoockLeft)
        {
            if(_sprite == null) return;
            _sprite.flipX = isLoockLeft;
        }

        public void Validate(GameObject obj)
        {
            _sprite = obj.GetComponentInChildren<SpriteRenderer>();
        }
    }
}