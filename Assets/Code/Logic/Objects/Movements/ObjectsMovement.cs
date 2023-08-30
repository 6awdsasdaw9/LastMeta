using Code.Data.AdditionalData;
using UnityEngine;

namespace Code.Logic.Objects.Movements
{
    public abstract class ObjectsMovement : MonoBehaviour
    {
        protected enum Axis
        {
            X,
            Y
        }

        [SerializeField] protected Axis CurrentAxis;
        [SerializeField] protected float Distance = 2;
        [SerializeField] protected RangedFloat RandomBonusDistance;
        [SerializeField] protected float Speed = 2;

        protected Vector3 TargetPosition;

        protected Vector3 RandomTargetPosition => CurrentAxis == Axis.X
            ? TargetPosition + new Vector3(RandomBonusDistance.GetRandom(), 0, 0)
            : TargetPosition + new Vector3(0,RandomBonusDistance.GetRandom(), 0);
        

        protected virtual void SetPositions()
        {
            TargetPosition = CurrentAxis switch
            {
                Axis.X => transform.position + Vector3.right * Distance,
                Axis.Y => transform.position + Vector3.up * Distance,
                _ => transform.position
            };
        }

        protected abstract void StartMove();
        protected abstract void Move();
        protected abstract void StopMove();

      
    }
}