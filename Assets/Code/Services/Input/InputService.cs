using System;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

namespace Code.Services.Input
{
    public class InputService : ITickable
    {
        private readonly InputMaster _master;
        private readonly EventSystem _eventSystem;

        private bool IsEnterPressed { get; set; }
        private bool _isInteractPressed;
        private bool _isPressedOnUI;

        public InputService()
        {
            _master = new InputMaster();
            _eventSystem = EventSystem.current;
            EneblePlayerInput();
            EnableUIInput(true);
            SubscribeToEvents();
        }

        public void Tick()
        {
            _isPressedOnUI = _eventSystem.IsPointerOverGameObject();
        }

        private void SubscribeToEvents()
        {
            _master.Player.Horizontal.performed += Movement;
            _master.Player.Horizontal.canceled += Movement;

            _master.Player.Dash.started += Dash;
            _master.Player.Attack.started += Attack;
            _master.Player.Attack.canceled += StopAttack;
            _master.Player.Jump.started += Jump;
            _master.Player.Crouch.started += Crouch;
            _master.Player.Crouch.canceled += Crouch;

            _master.Player.Skill_One.started += SkillButton_One;
            _master.Player.Skill_Two.started += SkillButton_Two;

            _master.UI.Esc.started += PressEsc;

            _master.UI.Interact.performed += InteractButtonPressed;
            _master.UI.Interact.canceled += InteractButtonPressed;

            _master.UI.Enter.performed += EnterButtonPressed;
            _master.UI.Enter.canceled += EnterButtonPressed;
        }


        #region Player input

        private void EneblePlayerInput() => _master.Player.Enable();
        public void DisablePlayerInput() => _master.Player.Disable();

        private void Movement(InputAction.CallbackContext context) => PlayerMovementEvent?.Invoke(context);
        public event Action<InputAction.CallbackContext> PlayerMovementEvent;

        private void Crouch(InputAction.CallbackContext context) => PlayerCrouchEvent?.Invoke(context);
        public event Action<InputAction.CallbackContext> PlayerCrouchEvent;

        private void Jump(InputAction.CallbackContext context) => PlayerJumpEvent?.Invoke(context);
        public event Action<InputAction.CallbackContext> PlayerJumpEvent;

        private void Dash(InputAction.CallbackContext context) => PlayerDashEvent?.Invoke();
        public event Action PlayerDashEvent;

        private void Attack(InputAction.CallbackContext context)
        {
            if (_isPressedOnUI)
                return;

            PlayerAttackEvent?.Invoke();
        }

        public event Action PlayerAttackEvent;

        private void SkillButton_One(InputAction.CallbackContext context) => SkillButton_1_Event?.Invoke();
        public event Action SkillButton_1_Event;

        private void SkillButton_Two(InputAction.CallbackContext context) => SkillButton_2_Event?.Invoke();
        public event Action SkillButton_2_Event;

        private void StopAttack(InputAction.CallbackContext context) => PlayerStopShootEvent?.Invoke();
        public event Action PlayerStopShootEvent;

        #endregion

        #region UI input

        private void EnableUIInput(bool activate)
        {
            if (activate) _master.UI.Enable();
            else _master.UI.Disable();
        }

        public void SimulatePressEsc()
        {
            OnPressEsc?.Invoke();
        }

        private void PressEsc(InputAction.CallbackContext context)
        {
            if (context.started)
                OnPressEsc?.Invoke();
        }
        public event Action OnPressEsc;


        private void InteractButtonPressed(InputAction.CallbackContext context)
        {
            _isInteractPressed = true;
            if (context.canceled)
                _isInteractPressed = false;
        }

        public bool GetInteractPressed()
        {
            var result = _isInteractPressed;
            _isInteractPressed = false;
            return result;
        }

        private void EnterButtonPressed(InputAction.CallbackContext context) =>
            IsEnterPressed = context.performed;

        public bool GetEnterPressed()
        {
            var result = IsEnterPressed;
            IsEnterPressed = false;
            return result;
        }

        #endregion
    }
}