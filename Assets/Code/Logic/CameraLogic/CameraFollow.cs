using System.Collections;
using Code.Character.Hero;
using Code.Character.Hero.HeroInterfaces;
using Code.Data;
using Code.Data.GameData;
using Code.Services;
using Code.Services.SaveServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Logic.CameraLogic
{
    public class CameraFollow : MonoBehaviour, ISavedData
    {
        [SerializeField] private bool _isTeleportToPlayerOnAwake =true;
        [SerializeField] private bool _isCanMove = true;
        [SerializeField] private bool _isCanMoveY = true;
        [SerializeField] private float _dampTime = 0.75f;
        private readonly float _maxDampTime = 1.5f;
        private float _currentDampTime;
        private readonly Vector3 _cameraOffset = new(0.5f, 1.6f, -60f);

        private float startPosY;
        
        private IHero _hero;
        private Vector3 _velocity = Vector3.zero;
        private Vector3 _target;

        private Vector3 _followingPosition => _hero.Transform.position + _cameraOffset;

        private Coroutine _dampTimeCoroutine;
        
        [Inject]
        private void Construct(IHero hero,SavedDataStorage dataStorage)
        {
            _hero = hero;
            _currentDampTime = _dampTime;
            dataStorage.Add(this);
        }

        private void LateUpdate()
        {
            if (_hero == null || !_isCanMove)
                return;
            
            _target = _isCanMove ? GetTarget() : transform.position;

            CameraMove();
        }

        private void CameraMove()
        {
            Vector3 horizontalTarget = new Vector3(_target.x, transform.position.y, _cameraOffset.z);
            transform.position = Vector3.SmoothDamp(transform.position, horizontalTarget, ref _velocity, _currentDampTime);
            
            Vector3 verticalTarget = new Vector3(transform.position.x, _target.y, _cameraOffset.z);
            var verticalDampTime = _target.y > transform.position.y ? _currentDampTime : _currentDampTime * 0.7f;
            transform.position = Vector3.SmoothDamp(transform.position, verticalTarget, ref _velocity,verticalDampTime);
        }

        private Vector3 GetTarget()
        {
            var y = _isCanMoveY ? _followingPosition.y : startPosY;
           
            y += _isCanMoveY && _hero.Movement.IsCrouch ? -1 : 0;
          
            return new Vector3(_followingPosition.x,y,_cameraOffset.z);
        }

        public void StopFollow()
        {
            _isCanMove = false;
            _dampTimeCoroutine = null;
            _currentDampTime = _maxDampTime;
        }

        public void StartFollow()
        {
            _isCanMove = true;
            _dampTimeCoroutine ??= StartCoroutine(ResetDampTimeCoroutine());
        }

        private IEnumerator ResetDampTimeCoroutine()
        {
            while (_currentDampTime > _dampTime)
            {
                _currentDampTime -= 0.01f;
                yield return new WaitForSeconds(0.01f);
            }

            _dampTimeCoroutine = null;
        }

        #region Save Progress

        public void LoadData(SavedData savedData)
        {
            if (!savedData.CameraPosition.positionInScene.ContainsKey(CurrentLevel()))
                return;

            Vector3Data savedPosition = savedData.CameraPosition.positionInScene[CurrentLevel()];
            transform.position = savedPosition.AsUnityVector();
            
            if (_isTeleportToPlayerOnAwake)
            {
                transform.position = _followingPosition;
            }
        }

        public void SaveData(SavedData savedData)
        {
            if (savedData.CameraPosition.positionInScene.ContainsKey(CurrentLevel()))
            {
                savedData.CameraPosition.positionInScene[CurrentLevel()] = transform.position.AsVectorData();
            }
            else
            {
                savedData.CameraPosition.AddPosition(CurrentLevel(), transform.position.AsVectorData());
            }
        }

        private string CurrentLevel() =>
            SceneManager.GetActiveScene().name;

        #endregion
    }
}