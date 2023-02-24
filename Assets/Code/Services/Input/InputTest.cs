using Code.Character.Hero;
using UnityEngine;

namespace Code.Services.Input
{
    public class InputTest : MonoBehaviour
    {
        [SerializeField] private HeroMovement move;
        [SerializeField] private HeroJump jump;
       
        //private InputService testInput;


        private void Start()
        {
    
            
        }

        /*private void OnEnable()
        {
            testInput = new InputService();
            testInput.PlayerJumpEvent += context =>  jump.OnJump(context);
            testInput.PlayerCrochEvent += context => move.OnCrouch(context);
            testInput.PlayerMovementEvent += context => move.OnMovement(context);
        }

        private void OnDisable()
        {
            testInput.PlayerJumpEvent -= context =>  jump.OnJump(context);
            testInput.PlayerCrochEvent -= context => move.OnCrouch(context);
            testInput.PlayerMovementEvent -= context => move.OnMovement(context);
        }*/
    }


}