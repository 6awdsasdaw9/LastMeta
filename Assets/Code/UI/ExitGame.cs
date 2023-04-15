using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    [SerializeField] private Button _button;
    
    void Start()
    {
           _button.onClick.AddListener(Exit);
    }

    private void Exit()
    {
        Application.Quit();
    }
}
