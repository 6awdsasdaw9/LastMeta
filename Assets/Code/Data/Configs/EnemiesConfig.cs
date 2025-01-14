using System;
using System.Linq;
using Code.Audio.AudioPath;
using Code.Character.Enemies;
using Code.Character.Enemies.EnemiesFacades;
using Code.Data.GameData;
using Code.Logic.Common;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Data.Configs
{
    [CreateAssetMenu(fileName = "EnemiesConfig", menuName = "ScriptableObjects/GameData/EnemiesConfig")]
    public class EnemiesConfig : ScriptableObject
    {
        public float CollisionDamage = 3f;
        [ListDrawerSettings(Expanded = false, ShowIndexLabels = true, ShowPaging = false, ShowItemCount = true)]
        [GUIColor(0.9f, 0.9f, 0.9f)] public EnemyData[] EnemiesData;
        [GUIColor(0.9f, 0.7f, 0.9f)] public EnemyFacade[] Prefabs;
        
        public EnemyFacade GetFacadeByType(EnemyType type)
        {
            return Prefabs.FirstOrDefault(d => d.Type == type);
        }
        
        public EnemyData GetDataByType(EnemyType type)
        {
            return EnemiesData.FirstOrDefault(d => d.Type == type);
        }

        public EnemyData GetRandom()//todo peredelat`
        {
            return EnemiesData[Random.Range(0, EnemiesData.Length)];
        }
    }

    [Serializable]
    public class EnemyData
    {
        public EnemyType Type;
        public HealthData HealthData;

        [Space, Title("Attack")] 
        public CollisionAttackData CollisionAttackData;
        [GUIColor(0.8f, 0.5f, 0.2f), SerializeField]
        private bool _isHasMelleAttack;
        [GUIColor(0.8f, 0.5f, 0.2f), ShowIf(nameof(_isHasMelleAttack))]
        public AttackData MelleAttackData;

        [Space] [GUIColor(0.2f, 0.8f, 0.5f), SerializeField]
        private RangeAttackType _rangeAttackType;
        [GUIColor(0.2f, 0.8f, 0.5f), ShowIf("@_rangeAttackType==RangeAttackType.Default")]
        public AttackData RangeAttackData;
        [GUIColor(0.2f, 0.8f, 0.5f), ShowIf("@_rangeAttackType==RangeAttackType.Spike")]
        public SpikeAttackData SpikeAttackData;

        [Space, Title("Move")] 
        [GUIColor(0.2f, 0.5f, 0.8f)] [Range(0, 10)]
        public float MoveSpeed = 3;

        [GUIColor(0.2f, 0.5f, 0.8f)] [Range(0, 10)]
        public float PatrolSpeed = 3;

        [GUIColor(0.2f, 0.5f, 0.8f)] [Range(0, 3)]
        public float PatrolCooldown = 3;

        [Space, Title("Assets")]
        [GUIColor(0.8f, 0.5f, 0.8f)]
        public EnemyAudioPath AudioPath;
    }

    public enum EnemyType
    {
        Spider,
        BlackHand
    }
}