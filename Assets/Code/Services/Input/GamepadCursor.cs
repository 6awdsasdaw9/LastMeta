using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

namespace Code.Services.Input
{
    public class GamepadCursor : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private RectTransform curcorTransform;
        [SerializeField] private RectTransform canvasRectTransform;
        [SerializeField] private Canvas canvas;
        [SerializeField] private float cursorSpeed = 1000;
        [SerializeField] private float padding = 15;

        private bool previousMouseState;
        private Mouse currentMouse;
        private Mouse virtualMouse;
        private Camera mainCamera;

        private string previousControlScheme = "";
        private const string gamepadScheme = "Gamepad";
        private const string mouseScheme = "Keyboard&Mouse";

        private void OnEnable()
        {
            currentMouse = Mouse.current;
            mainCamera = Camera.main;

            if (virtualMouse == null)
                virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");
            else if (!virtualMouse.added)
                InputSystem.AddDevice(virtualMouse);

            InputUser.PerformPairingWithDevice(virtualMouse, playerInput.user);

            if (curcorTransform != null)
            {
                Vector2 position = curcorTransform.anchoredPosition;
                InputState.Change(virtualMouse.position, position);
            }

            InputSystem.onAfterUpdate += UpdateMotion;
            playerInput.onControlsChanged += OnControlsChanged;
            OnControlsChanged(playerInput);
        }

        void OnDisable()
        {
            if (virtualMouse != null && virtualMouse.added) InputSystem.RemoveDevice(virtualMouse);
            InputSystem.onAfterUpdate -= UpdateMotion;
            playerInput.onControlsChanged -= OnControlsChanged;
        }

        private void UpdateMotion()
        {
            if (virtualMouse == null || Gamepad.current == null) return;

            Vector2 deltaValue = Gamepad.current.rightStick.ReadValue();
            deltaValue *= cursorSpeed * Time.unscaledDeltaTime;

            Vector2 currentPosition = virtualMouse.position.ReadValue();
            Vector2 newPosition = currentPosition + deltaValue;

            newPosition.x = Mathf.Clamp(newPosition.x, padding, Screen.width - padding);
            newPosition.y = Mathf.Clamp(newPosition.y, padding, Screen.height - padding);

            InputState.Change(virtualMouse.position, newPosition);
            InputState.Change(virtualMouse.delta, deltaValue);

            bool aRightStickPress = Gamepad.current.rightStickButton.isPressed;
            if (previousMouseState != aRightStickPress)
            {
                virtualMouse.CopyState<MouseState>(out var mouseState);
                mouseState.WithButton(MouseButton.Left, aRightStickPress);
                InputState.Change(virtualMouse, mouseState);
                previousMouseState = aRightStickPress;
            }

            AnchourCoursor(newPosition);
        }

        private void AnchourCoursor(Vector2 position)
        {
            Vector2 anchoredPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, position,
                canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : mainCamera, out anchoredPosition);
            curcorTransform.anchoredPosition = anchoredPosition;
        }

        private void OnControlsChanged(PlayerInput input)
        {
            if (playerInput.currentControlScheme == mouseScheme && previousControlScheme != mouseScheme)
            {
                curcorTransform.gameObject.SetActive(false);
                Cursor.visible = true;
                currentMouse.WarpCursorPosition(virtualMouse.position.ReadValue());
                previousControlScheme = mouseScheme;
            }
            else if (playerInput.currentControlScheme == gamepadScheme && previousControlScheme != gamepadScheme)
            {
                curcorTransform.gameObject.SetActive(true);
                Cursor.visible = false;
                InputState.Change(virtualMouse.position, currentMouse.position.ReadValue());
                AnchourCoursor(currentMouse.position.ReadValue());
                previousControlScheme = gamepadScheme;
            }
        }
    }
}