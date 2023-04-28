using System;
using System.Collections;
using Code.Data.Configs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code
{
    public class Test : MonoBehaviour
    {
      //=)
      [SerializeField] private SomeLogic _logic;
      [SerializeField] private GameObject _object;

        private void Start()
        {
            _logic.Init(_object);
        }
    }

    [Serializable]
    public class SomeLogic
    {
        private GameObject _logicObject;

        public void Init(GameObject someObject)
        {
            _logicObject = someObject;
        }
    }
}