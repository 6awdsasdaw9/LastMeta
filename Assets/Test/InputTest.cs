using Code.Services.Input;
using Test.Platformer_Toolkit_Character_Controller;
using UnityEngine;

namespace Test
{
    public class InputTest : MonoBehaviour
    {
        [SerializeField] private characterMovement move;
        [SerializeField] private characterJump jump;

        private InputService testInput;


        private void Start()
        {
            //testInput.test.Player.testJump.performed += ctx => jump.OnJump(ctx);
            testInput = new InputService();
            testInput.PlayerJumpEvent += context =>  jump.OnJump(context);
            testInput.PlayerJumpEvent += context =>  jump.OnJump(context);
      
        }

     

        void Update()
        {
            float horizontal = testInput.horizontalAxis;
            /*if (testInput.verticalAxis > 0)
            {
                fdfsdfdsfs();
            }*/
            move.directionX = horizontal;
        }
    }
}