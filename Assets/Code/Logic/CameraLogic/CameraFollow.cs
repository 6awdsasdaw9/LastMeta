using System.Collections;
using Code.Character.Hero;
using Code.Data.ProgressData;
using Code.Data.States;
using Code.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Logic.CameraLogic
{
    public class CameraFollow : MonoBehaviour, ISavedData
    {
        [SerializeField] private bool _isCanMoveY = true;
        
        [SerializeField] private float _dampTime = 0.75f;
        private readonly float _maxDampTime = 1.5f;
        private float _currentDampTime;

        private float startPosY;
        private bool _isCanMove = true;

        private Transform _following;
        private Vector3 _velocity = Vector3.zero;
        private Vector3 _target;
        
        private readonly Vector3 _cameraOffset = new(0.5f,  1.6f, -60f);
        private Vector3 _followingPosition => _following.position + _cameraOffset;

        private Coroutine _dampTimeCoroutine;


        [Inject]
        private void Construct(HeroMovement hero, SavedDataCollection dataCollection)
        {
            _following = hero.transform;
            _currentDampTime = _dampTime;
            
            dataCollection.Add(this);
        }
        
        private void LateUpdate()
        {
            if (_following == null)
                return;

            var x = _followingPosition.x;
            var y = _isCanMoveY ? _followingPosition.y : startPosY;
            var z = _cameraOffset.z;

            if (_isCanMove) 
                _target = new Vector3(x, y, z);
          
            else
            {
                _target = transform.position;
            }
            
            transform.position = Vector3.SmoothDamp(transform.position, _target, ref _velocity, _currentDampTime);
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
            if (savedData.cameraPositionData.scene != CurrentLevel() ||
                savedData.cameraPositionData.position?.AsUnityVector() == Vector3.zero)
                return;

            Vector3Data savedPosition = savedData.cameraPositionData.position;
            _target = savedPosition.AsUnityVector();
            transform.position = _target;
        }

        public void SaveData(SavedData savedData)
        {
            savedData.cameraPositionData =
                new PositionData(CurrentLevel(), _target.AsVectorData());
        }
        
        private string CurrentLevel() =>
            SceneManager.GetActiveScene().name;
       
        #endregion
    }
}