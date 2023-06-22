using System;
using Code.Data.GameData;
using Code.Services;
using Code.Services.SaveServices;
using UnityEngine;

namespace Code.Logic.Objects.DestroyedObjects
{
    public class DestroyedObjectController : MonoBehaviour, IEventSubscriber, ISavedData
    {
        [SerializeField] private UniqueId _id;
        [SerializeField] private Collider _collider;
        [SerializeField] private DestroyedObjectHealth _health;
        [SerializeField] private DestroyedObjectSpriteParam[] _destroyedSprites;
        private bool _isDestroyed;

        private void OnEnable() => SubscribeToEvent(true);
        private void OnDisable() => SubscribeToEvent(false);

        public void SubscribeToEvent(bool flag)
        {
            if (flag)
            {
                _health.OnHealthChanged += HealthOnOnHealthChanged;
            }
            else
            {
                _health.OnHealthChanged -= HealthOnOnHealthChanged;
            }
        }

        private void HealthOnOnHealthChanged()
        {
            if (_isDestroyed || _health.Current > 0)
                return;

            _isDestroyed = true;
            foreach (var objectSpriteParam in _destroyedSprites)
            {
                objectSpriteParam.Animation.PlayDestroy();
            }

            Disable();
        }

        private void Disable()
        {
            enabled = false;
            _collider.enabled = false;

            foreach (var objectSpriteParam  in _destroyedSprites)
            {
                objectSpriteParam.Animation.enabled = false;
            }
        }

        public void LoadData(SavedData savedData)
        {
            if (!savedData.DestroyedObjects.ContainsKey(_id.Id))
                return;

            _isDestroyed = savedData.DestroyedObjects[_id.Id];

            if (!_isDestroyed)
                return;

            foreach (var objectSprite in _destroyedSprites)
            {
                objectSprite.SpriteRenderer.sprite = objectSprite.DestroyedImage;
            }

            Disable();
        }

        public void SaveData(SavedData savedData)
        {
            if (savedData.DestroyedObjects.ContainsKey(_id.Id))
            {
                savedData.DestroyedObjects[_id.Id] = _isDestroyed;
            }
            else
            {
                savedData.DestroyedObjects.Add(_id.Id, _isDestroyed);
            }
        }

        [Serializable]
        private class DestroyedObjectSpriteParam
        {
            public SpriteRenderer SpriteRenderer;
            public Sprite DestroyedImage;
            public DestroyedObjectAnimation Animation;
        }
    }
}