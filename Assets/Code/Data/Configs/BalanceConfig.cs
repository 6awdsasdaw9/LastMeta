using Code.Data.Stats;
using UnityEngine;

namespace Code.Data.Configs
{
    [CreateAssetMenu(fileName = "Balance Config", menuName = "ScriptableObjects/GameSettings/Balance Config")]
    public class BalanceConfig : ScriptableObject
    {
        public PlayerConfig playerConfig;
    }
}