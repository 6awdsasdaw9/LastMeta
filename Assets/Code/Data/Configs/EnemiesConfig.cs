using System;
using System.Linq;
using Code.Audio.AudioPath;
using Code.Character.Common.CommonCharacterInterfaces;
using Code.Data.GameData;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Data.Configs
{
    [CreateAssetMenu(fileName = "EnemiesConfig", menuName = "ScriptableObjects/GameData/EnemiesConfig")]
    public class EnemiesConfig : ScriptableObject
    {
        public EnemyData[] EnemiesData;

        public EnemyData GetByType(EnemyType type)
        {
            return EnemiesData.FirstOrDefault(d => d.Type == type);
        }
        public EnemyData GetRandom()
        {
            return EnemiesData[Random.Range(0, EnemiesData.Length)];
        }
    }

    [Serializable]
    public class EnemyData
    {
        public EnemyType Type;
        [Title("Params")] 
        public HealthData HealthData;
        [Space] 
        public DamageParam DamageParam;
        public PushData PushData;
        [Space]
        [Range(0, 10)] public float MoveSpeed = 3;
        [Range(0, 10)] public float PatrolSpeed = 3;
        [Range(0, 3)] public float PatrolCooldown = 3;

        [Space,Title("Assets")]
        public EnemyAudioPath AudioPath;
        public GameObject Prefab;
    }

    public enum EnemyType
    {
        Spider,
        BlackHand
    }
}