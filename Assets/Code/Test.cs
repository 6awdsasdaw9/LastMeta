using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code
{
    public class Test : MonoBehaviour
    {
      //=)

      /*
      private Coroutine _firstCoroutine;
      private Coroutine _secondCoroutine;

      private void Update()
      {
          if (Input.GetKeyDown(KeyCode.E))
          {
              Debug.Log("Press");
              _firstCoroutine ??= StartCoroutine(Check(1));
              if (_firstCoroutine != null)
              {
                  _secondCoroutine = StartCoroutine(Check(2));
              }
   
          }
      }

      private IEnumerator Check(int id)
      {
          Debug.Log($"Start Coroutine {id}");
          yield return new WaitForSeconds(3f);
          Debug.Log($"Stop Coroutine {id}");
      }

      [Button]
      private void TestCoroutine()
      {
          Debug.Log("Coroutin is null " + (_firstCoroutine == null));
      }*/
    }
}