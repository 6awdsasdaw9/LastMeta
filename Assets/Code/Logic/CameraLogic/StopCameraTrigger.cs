using System;
using Code.Logic.CameraLogic;
using UnityEngine;

public class StopCameraTrigger : MonoBehaviour
{
    [SerializeField] private CameraFollow _cameraFollow;

    private void OnTriggerEnter(Collider other)
    {
        _cameraFollow.CameraHandler(false);
        Debug.Log("VARVAR");
    }

    private void OnTriggerExit(Collider other)
    {
        _cameraFollow.CameraHandler(true);
    }
}
