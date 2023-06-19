using System;
using Code.Debugers;
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
            EnablePlayerInput();
            EnableUIInput(true);
            SubscribeToEvents();
        }

        public void Tick()
        {
            _isPressedOnUI = _eventSystem.IsPointerOverGameObject();
        }

        private void SubscribeToEvents()
        {
            _master.Player.Horizontal.performed += PressMovementEvent;
            _master.Player.Horizontal.canceled += PressMovementEvent;

            _master.Player.Dash.started += PressDashEvent;
            _master.Player.Attack.started += PressAttackEvent;
            _master.Player.Attack.canceled += UnPressAttackEvent;
            _master.Player.Jump.started += PressJumpEvent;
            _master.Player.Crouch.started += PressCrouchEvent;
            _master.Player.Crouch.canceled += PressCrouchEvent;

            _master.Player.Skill_One.started += PressSkillButtonOneEvent;
            _master.Player.Skill_Two.started += PressSkillButtonTwoEvent;

            _master.UI.Esc.started += PressEscEvent;

            _master.UI.Interact.performed += InteractButtonPressedEvent;
            _master.UI.Interact.canceled += InteractButtonPressedEvent;

            _master.UI.Enter.performed += EnterButtonPressedEvent;
            _master.UI.Enter.canceled += EnterButtonPressedEvent;
        }


        #region Player input

        private void EnablePlayerInput() => _master.Player.Enable();

        public void DisablePlayerInput() => _master.Player.Disable();

        private void PressMovementEvent(InputAction.CallbackContext context) => OnPressMovement?.Invoke(context);

        public float GetDirection()
        {
            return _master.Player.Horizontal.ReadValue<float>();
        }

        public event Action<InputAction.CallbackContext> OnPressMovement;

        private void PressCrouchEvent(InputAction.CallbackContext context) => OnPressCrouch?.Invoke(context);

        public event Action<InputAction.CallbackContext> OnPressCrouch;

        private void PressJumpEvent(InputAction.CallbackContext context) => OnPressJump?.Invoke(context);

        public event Action<InputAction.CallbackContext> OnPressJump;

        private void PressDashEvent(InputAction.CallbackContext context) => OnPressDash?.Invoke();

        public event Action OnPressDash;

        private void UnPressAttackEvent(InputAction.CallbackContext obj)
        {
            if (_isPressedOnUI)
                return;
            OnUnPressAttackButton?.Invoke();
        }
        public event Action OnUnPressAttackButton;

        private void PressAttackEvent(InputAction.CallbackContext context)
        {
            if (_isPressedOnUI)
                return;
            
            OnPressAttackButton?.Invoke();
        }

        public event Action OnPressAttackButton;

        private void PressSkillButtonOneEvent(InputAction.CallbackContext context) => OnPressSkillButtonOne?.Invoke();

        public event Action OnPressSkillButtonOne;

        private void PressSkillButtonTwoEvent(InputAction.CallbackContext context) => OnPressSkillButtonTwo?.Invoke();

        public event Action OnPressSkillButtonTwo;

        private void PressSkillButtonThreeEvent(InputAction.CallbackContext context) =>
            OnPressSkillButtonThree?.Invoke();

        public event Action OnPressSkillButtonThree;

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

        private void PressEscEvent(InputAction.CallbackContext context)
        {
            if (context.started)
                OnPressEsc?.Invoke();
        }

        public event Action OnPressEsc;


        private void InteractButtonPressedEvent(InputAction.CallbackContext context)
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

        private void EnterButtonPressedEvent(InputAction.CallbackContext context) =>
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