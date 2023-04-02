using System;
using DG.Tweening;
using UnityEngine;

namespace Code.Objects
{
    public class ObjectRotate : MonoBehaviour
    {
        private enum Axis
        {
            X,
            Y,
            Z,
            XY
        }

        [SerializeField] private Axis _axis;
        [SerializeField, Range(0, 360)] private float angle = 360;
        [SerializeField] private float _cycleLength = 10;

        private void Start()
        {
            transform.DORotate(GetAngle(), _cycleLength/*,RotateMode.FastBeyond360*/).SetLoops(-1, LoopType.Restart);
        }

        private Vector3 GetAngle()
        {
            switch (_axis)
            {
                case Axis.X:
                    return new Vector3(angle, 0, 0);
                case Axis.Y:
                    return new Vector3(0, angle, 0);
                case Axis.Z:
                    return new Vector3(0, angle, 0); 
                case Axis.XY:
                    return new Vector3(angle, angle, 0);
            }

            return Vector3.zero;
        }
    }
}