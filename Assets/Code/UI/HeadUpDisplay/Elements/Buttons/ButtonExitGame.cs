using UnityEngine;

namespace Code.UI.HeadUpDisplay.Elements.Buttons
{
    public class ButtonExitGame : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Button _button;

        private void Start()
        {
            _button.onClick.AddListener(Application.Quit);
        }
    }
}