using System;
using System.Collections.Generic;
using Code.Data.GameData;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Data.States
{
    [Serializable]
    public class HeroConfig
    {
        [Title("Health")]
        public float maxHP = 20;

        [Title("Damage")] 
        public PowerData power;
        
        [Title("Move")]
        [Range(0f, 20f)] public float maxSpeed = 2.5f;
        [Range(0f, 1f)] public float crouchSpeed = 0.7f;
        [Range(0f, 100f)] public float maxAcceleration = 16f;
        [Range(0f, 100f)] public float maxDeceleration = 52f;
        [Range(0f, 100f)] public float maxTurnSpeed = 60;
        [Range(0f, 100f)] public float maxAirAcceleration = 30;
        [Range(0f, 100f)] public float maxAirDeceleration = 45;
        [Range(0f, 100f)] public float maxAirTurnSpeed = 60;

        [Title("Physics Material")] 
        public PhysicMaterial NoFrictionMaterial;
        public PhysicMaterial FrictionMaterial;
        
        [Title("Jump")] 
        [Range(2f, 5.5f), Tooltip("Максимальная высота прыжка")]
        public float jumpHeight = 3;
        [Range(0.2f, 1.25f), Tooltip("Время, за которое достигается максимальная высота")]
        public float timeToJumpApex = 0.8f;
        [Range(0f, 5f), Tooltip("Множитель гравитации при движении вверх")]
        public float upwardMovementMultiplier = 3;
        [Range(1f, 10f), Tooltip("Множитель гравитации при движении вниз")]
        public float downwardMovementMultiplier = 2f;
        [Range(0, 1), Tooltip("Сколько раз можно прыгнуть в воздухе?")]
        public int maxAirJumps;
        [Range(1f, 10f), Tooltip("Множитель гравитации, когда отпускаешь кнопку прыжка")]
        public float jumpCutOff = 3;
        [Tooltip("Максимальная скорость падения персонажа")]
        public float speedLimit = 6.7f;
        [Range(0f, 0.3f), Tooltip("Сколько должно длиться время койота?")]
        public float coyoteTime = 0.1f;
        [Range(0f, 0.3f), Tooltip("Как далеко от земли кушируется прыжок?")]
        public float jumpBuffer = 0.3f;
        public HeroParamData[] UpgradeParams;

    }

    [Serializable]
    public class HeroParamData
    {
        public UpgradeParamType Type;
        //TODO Переделать в айди
        public List<UpgradeParamData> Params;
    }

    public enum UpgradeParamType
    {
        Speed,
        JumpHeight
    }

    [Serializable]
    public class UpgradeParamData
    {
        //TODO Переделать в айди
        public int Lvl;
        public float Value;
    }
}