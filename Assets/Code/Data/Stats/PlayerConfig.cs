using System;
using UnityEngine;

namespace Code.Data.Stats
{
    [Serializable]
    public class PlayerConfig
    {
        [Title("Movement Stats")]
        [Range(0f, 20f)] public float maxSpeed = 10f;
        [Range(0f, 1f)] public float _crouchSpeed = 0.5f;
        [Range(0f, 100f)] public float _maxAcceleration = 52f;
        [Range(0f, 100f)] public float _maxDeceleration = 52f;
        [Range(0f, 100f)] public float _maxTurnSpeed = 80f;
        [Range(0f, 100f)] public float _maxAirAcceleration;
        [Range(0f, 100f)] public float _maxAirDeceleration;
        [Range(0f, 100f)] public float _maxAirTurnSpeed = 80f;
    }
}