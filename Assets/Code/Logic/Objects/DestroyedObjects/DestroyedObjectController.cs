using System;
using Code.Audio.AudioEvents;
using Code.Data.AdditionalData;
using Code.Data.GameData;
using Code.Debugers;
using Code.Logic.Objects.Animations;
using Code.Services;
using Code.Services.SaveServices;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Logic.Objects.DestroyedObjects
{
    public class DestroyedObjectController : MonoBehaviour, IEventsSubscriber, ISavedData
    {
        [InfoBox("Is saved data")]
        [SerializeField] private UniqueId _id;
        [SerializeField] private Collider _collider;
        [SerializeField] private DestroyedObjectHealth _health;
        [SerializeField] private DestroyedObjectSpriteParam[] _destroyedSprites;
        [GUIColor(0.85f, 0.74f, 1)]
        [SerializeField] private AudioEvent _audioEvent;

        private bool _isDestroyed;

        [Inject]
        private void Construct(SavedDataStorage savedDataStorage, EventSubsribersStorage eventSubsribersStorage)
        {
            savedDataStorage.Add(this);
            eventSubsribersStorage.Add(this);
        }

        private void OnEnable() => SubscribeToEvents(true);
        private void OnDisable() => SubscribeToEvents(false);

        public void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _health.OnHealthChanged += OnHealthChanged;
            }
            else
            {
                _health.OnHealthChanged -= OnHealthChanged;
            }
        }

        private void OnHealthChanged()
        {
            if (_isDestroyed || _health.Current > 0)
                return;

            _isDestroyed = true;
            foreach (var objectSpriteParam in _destroyedSprites)
            {
                objectSpriteParam.Animation.PlayDestroy();
                _audioEvent.PlayAudioEvent();
            }

            Disable();
        }

        private void Disable()
        {
            enabled = false;
            _collider.enabled = false;

            foreach (var objectSpriteParam in _destroyedSprites)
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

            gameObject.SetActive(false);
            /*foreach (var objectSprite in _destroyedSprites)
            {
                objectSprite.Animation.DisableAnimation();
                objectSprite.SpriteRenderer.enabled = false;
            }

            Disable();*/
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
            public DestroyedAnimation Animation;
        }
    }
}