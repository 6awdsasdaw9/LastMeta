using System;
using Code.Character.Hero;
using Code.Data.ProgressData;
using Code.Data.States;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Logic.CameraLogic
{
    public class CameraFollow : MonoBehaviour, ISavedData
    {
        [SerializeField] private float _dampTime = 0.75f;
        [SerializeField] private bool _isCanLookDown = true;
        [SerializeField] private bool _isCanMoveY = true;
        private float startPosY;
        private bool _isCanMove = true;

        private Transform _following;
        private Vector3 _velocity = Vector3.zero;
        private Vector3 _target;

        private const float offsetX = 0.5f;
        private const float offsetY = 1.6f;

        [SerializeField] private LayerMask stopLayer;
        
        
        [Inject]
        private void Construct(HeroMovement hero, SavedDataCollection dataCollection)
        {
            _following = hero.transform;
            dataCollection.Add(this);
        }

        /*private void Awake()
        {
            if (!_isCanMoveY)
                startPosY = transform.position.y;


            if (_isCanMove)
            {
                _target = GetFollowingPosition();
                transform.position = _target;
            }
        }*/

        private void Update()
        {
         var ray =  Physics.Raycast(_following.position + Vector3.forward * -2, Vector3.forward, 2, stopLayer);
         Debug.DrawRay(_following.position  + Vector3.forward * -2, Vector3.forward * 2);
         Debug.Log(ray);
         CameraHandler(!ray);
        }

        private void LateUpdate()
        {
            if (_following == null)
                return;


            var x = _following.position.x + _cameraOffset.x;
            var y = _isCanMoveY ? _following.position.y + _cameraOffset.y : startPosY;
            var z = _cameraOffset.z;

            if (_isCanMove)
            {
                Debug.Log("Is can move");
                _target = new Vector3(x, y, z);
                
            }
                

            transform.position = Vector3.SmoothDamp(transform.position, _target, ref _velocity, _dampTime);
        }


        private Vector3 GetFollowingPosition()
        {
            return _following.position + _cameraOffset;
        }

        private Vector3 _cameraOffset => new Vector3(offsetX, offsetY, -60f);

        public void CameraHandler(bool active)
        {
            _isCanMove = active;
        }

        public void Follow(GameObject following)
        {
            _following = following.transform;
        }

        public void LoadData(SavedData savedData)
        {
            Debug.Log("aaaa Load camera");
            if (savedData.cameraPositionData.level != CurrentLevel() ||
                savedData.cameraPositionData.position.AsUnityVector() == Vector3.zero)
                return;

            Vector3Data savedPosition = savedData.cameraPositionData.position;
            //transform.position = savedPosition.AsUnityVector();
            
            Debug.Log("Load camera");
            _target = savedPosition.AsUnityVector();
            _isCanMove = savedData.cameraPositionData.isCanMove;
        }

        public void SaveData(SavedData savedData)
        {
            savedData.cameraPositionData =
                new CameraPositionData(CurrentLevel(), _target.AsVectorData(),_isCanMove);
            Debug.Log("Save camera");
        }
        
        private string CurrentLevel() =>
            SceneManager.GetActiveScene().name;
    }
}