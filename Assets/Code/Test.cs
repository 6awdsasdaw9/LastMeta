using System;
using Code.Logic.Interactive;
using UnityEngine;

namespace Code
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private IInteractive _interactiveObject;

        private void Start()
        {
            Debug.Log(gameObject.name);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(_interactiveObject == null)
                    Debug.Log("=(");
                else
                    Debug.Log("=)");
            }
        }
    }
}