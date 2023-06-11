using UnityEngine;
using UnityEngine.UI;

namespace Code.PresentationModel.Buttons
{
    public class ButtonExitGame : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Start()
        {
            _button.onClick.AddListener(Application.Quit);
        }
    }
}