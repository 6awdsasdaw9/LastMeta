using System;
using System.Linq;
using Code.Audio.AudioPath;
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
        [Range(1, 100)] public int Hp = 50;
        [Space]
        [Range(1, 30)] public float Damage = 10;
        [Range(.5f, 3)] public float EffectiveDistance = .5f;
        [Range(.5f, 2)] public float Cleavage = .5f;
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