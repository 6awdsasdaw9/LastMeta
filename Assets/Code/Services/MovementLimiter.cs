using System;

namespace Code.Services
{
    //Все классы, которым нужно остановить функционирование при паузе/смерти подписываются на ивенты этого класса,
    //экземляр которого создается в SceneContext как AsSingle 
    public class MovementLimiter
    {
        public bool CharactersCanMove { get; private set; }
        
        public Action OnDisableMovementMode;
        public Action OnEnableMovementMode;

        public MovementLimiter()
        {
            CharactersCanMove = true;
        }


        public void DisableMovement()
        {
            CharactersCanMove = false;
            OnDisableMovementMode?.Invoke();
        }

        public void EnableMovement()
        {
            CharactersCanMove = true;
            OnEnableMovementMode?.Invoke();
        }
    }
}