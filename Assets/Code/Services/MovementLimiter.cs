using System;

namespace Code.Character
{
    //Все классы, которым нужно остановить функционирование при паузе/смерти подписываются на ивенты этого класса,
    //экземляр которого создается в SceneContext как AsSingle 
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