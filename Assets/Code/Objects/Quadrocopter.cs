using System.Collections.Generic;
using UnityEngine;

namespace Code.Objects
{
    public class Quadrocopter : MonoBehaviour
    {
        [SerializeField]private Movement.Movement _movement;
        [SerializeField] private List<Transform> _points;
        private int _pointID;
  
        // Update is called once per frame
        void Update()
        {
            _movement.HorizontalMove(_points[_pointID].position);
        
            if(_movement.IsNear(_points[_pointID])) 
                SwitchPoint();
        
        }


        private void SwitchPoint()
        {
            if (_pointID == _points.Count - 1) _pointID = 0;
            else _pointID++;
        }
    
    }
}
