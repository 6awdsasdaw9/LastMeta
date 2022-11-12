using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Services.Input
{
    public class InputService
    {
        private InputMaster _master;
        public float horizontalAxis => _master.Player.Movement.ReadValue<Vector2>().x;
        public float verticalAxis => _master.Player.Movement.ReadValue<Vector2>().y;
        public Vector2 Axis => _master.Player.Movement.ReadValue<Vector2>();

        private bool _isInteractPressed = false;




        public InputService()
        {
            _master = new InputMaster();
            EneblePlayerInput();
            EnebleUIInput(true);

            ConnectToEvents();
        }

        /*
        private void OnDisable()
        {
            DisablePlayerInput();
            EnebleUIInput(false);
        }
        */


        private void ConnectToEvents()
        {
            _master.Player.Dash.started += Dash;
            _master.Player.Punch.started += Punch;
            _master.Player.Punch.canceled += StopShoot;
            _master.Player.testJump.started  +=  ctx => Jump(ctx);

            _master.Player.HandToHand.started += SkillButton_1;
            _master.Player.TakeAGun.started += SkillButton_2;


            _master.UI.MenuPause.started += OpenMenu;
            _master.UI.Interact.performed += InteractButtonPressed;
            _master.UI.Interact.canceled += InteractButtonPressed;

            /*EventManager.OnControlAllowedEvent += EneblePlayerInput;
            EventManager.OnControlProhibitedEvent += DisablePlayerInput;*/
        }


        /*private void UpdateHorizontal()
        {
            if (vector.x != 0 && vector.x != horizontal) horizontal = vector.x;
            else if (horizontal == 0) horizontal = -1;
        }*/

        #region Player input

        public void EneblePlayerInput() => _master.Player.Enable();
        public void DisablePlayerInput() => _master.Player.Disable();


        private void Jump(InputAction.CallbackContext context) => PlayerJumpEvent?.Invoke(context);
        public event Action<InputAction.CallbackContext> PlayerJumpEvent;

        private void Dash(InputAction.CallbackContext context) => PlayerDashEvent?.Invoke();
        public event Action PlayerDashEvent;

        private void Punch(InputAction.CallbackContext context)
        {
            /* if (_mainCamera?.ScreenToViewportPoint(Input.mousePosition).y >= 0.05f)*/
            {
                PlayerPunchEvent?.Invoke();
            }
        }

        public event Action PlayerPunchEvent;

        private void SkillButton_1(InputAction.CallbackContext context) => SkillButton_1_Event?.Invoke();
        public event Action SkillButton_1_Event;
        private void SkillButton_2(InputAction.CallbackContext context) => SkillButton_2_Event?.Invoke();
        public event Action SkillButton_2_Event;
        private void StopShoot(InputAction.CallbackContext context) => PlayerStopShootEvent?.Invoke();
        public event Action PlayerStopShootEvent;

        #endregion

        #region UI input

        public void EnebleUIInput(bool activate)
        {
            if (activate) _master.UI.Enable();
            else _master.UI.Disable();
        }

        private void OpenMenu(InputAction.CallbackContext context)
        {
            PauseEvent?.Invoke();
        }

        public event Action PauseEvent;

        private void InteractButtonPressed(InputAction.CallbackContext context)
        {
            /*if (context.started || context.performed)*/
            _isInteractPressed = true;
            if (context.canceled) _isInteractPressed = false;
            InteractButtonEvent?.Invoke();
        }

        public event Action InteractButtonEvent;

        public bool GetInteractPressed()
        {
            bool result = _isInteractPressed;
            _isInteractPressed = false;
            return result;
        }

        #endregion
    }
}