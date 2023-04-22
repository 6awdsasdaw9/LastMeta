using UnityEngine;
using UnityEngine.UI;

public class ButtonExitGame : MonoBehaviour
{
    [SerializeField] private Button _button;

    void Start()
    {
        _button.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}