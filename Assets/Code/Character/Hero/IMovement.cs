using UnityEngine.InputSystem;

namespace Code.Character.Hero
{
    public interface IMovement
    {
        void OnMovement(InputAction.CallbackContext context);
        void StopMovement();
    }
}