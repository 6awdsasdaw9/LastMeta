using System;

namespace Code.Character
{
    [Serializable]
    internal class MovementLimiter
    {
        public bool characterCanMove { get; private set; }
        public Action OnDisableMovementMode;
        public Action OnEnableMovementMode;
        public MovementLimiter(bool canMove)
        {
            characterCanMove = canMove;
        }


        public void DisableMovement()
        {
            characterCanMove = false;
            OnDisableMovementMode?.Invoke();
        } 
    }
}