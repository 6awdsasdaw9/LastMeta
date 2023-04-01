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
        [SerializeField] private bool _isCanMoveY = true;
        //[SerializeField] private bool _isCanLookDown = true;
      
        private float startPosY;
        private bool _isCanMove = true;

        private Transform _following;
        private Vector3 _velocity = Vector3.zero;
        private Vector3 _target;
        
        private readonly Vector3 _cameraOffset = new(0.5f,  1.6f, -60f);
        private Vector3 _followingPosition => _following.position + _cameraOffset;

        private LayerMask _stopCameraLayer; 

        [Inject]
        private void Construct(HeroMovement hero, SavedDataCollection dataCollection)
        {
            _following = hero.transform;
           
            _stopCameraLayer = 1 << LayerMask.NameToLayer(Constants.StopCameraLayer);
            
            dataCollection.Add(this);
        }
        
        private void Update()
        {
             CheckCameraBoarder();
        }


        private void CheckCameraBoarder()
        {
            if (HeroInsideTheBorder() && CameraInsideTheBorder())
            {
                _isCanMove = false;
            }
            else if (!HeroInsideTheBorder())
            {
                _isCanMove = true;
            }
        }
        
        
        private bool HeroInsideTheBorder() => 
            Physics.Raycast(_following.position + Vector3.forward * - 2, Vector3.forward, 2, _stopCameraLayer);
        
        private bool CameraInsideTheBorder()
        {
            Debug.DrawRay(transform.position,Vector3.forward * 65);
            return Physics.Raycast(transform.position, Vector3.forward, 65, _stopCameraLayer);
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
            
            transform.position = Vector3.SmoothDamp(transform.position, _target, ref _velocity, _dampTime);
        }
        
        public void LoadData(SavedData savedData)
        {
            if (savedData.cameraPositionData.scene != CurrentLevel() ||
                savedData.cameraPositionData.position.AsUnityVector() == Vector3.zero)
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
    }
}