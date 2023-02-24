using System;

namespace Code.Character
{
    public class MovementLimiter
    {
        public bool charactersCanMove { get; private set; }
        public Action OnDisableMovementMode;
        public Action OnEnableMovementMode;

        public MovementLimiter()
        {
            charactersCanMove = true;
        }


        public void DisableMovement()
        {
            charactersCanMove = false;
            OnDisableMovementMode?.Invoke();
        }

        public void EnableMovement()
        {
            charactersCanMove = true;
            OnEnableMovementMode?.Invoke();
        }
    }
}