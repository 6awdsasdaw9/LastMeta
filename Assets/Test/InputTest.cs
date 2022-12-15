using Code.Character.Hero;
using Code.Services.Input;
using Test.Hero;
using UnityEngine;

namespace Test
{
    public class InputTest : MonoBehaviour
    {
        [SerializeField] private HeroMovement move;
        [SerializeField] private HeroJump jump;
        [SerializeField] private checkJump _checkJump;
        private InputService testInput;


        private void Start()
        {
            testInput = new InputService();
          //  testInput.PlayerJumpEvent += context =>  _checkJump.OnJump(context);
           testInput.PlayerJumpEvent += context =>  jump.OnJump(context);
            testInput.PlayerMovementEvent += context => move.OnMovement(context);
            testInput.PlayerCrochEvent += context => move.OnCrouch(context);


        }
        
    }
}