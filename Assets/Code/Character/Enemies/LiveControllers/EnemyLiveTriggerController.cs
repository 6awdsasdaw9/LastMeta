using Code.Character.Enemies.EnemiesFacades;
using Code.Logic.Collisions.Triggers;
using UnityEngine;

namespace Code.Character.Enemies
{
    public class EnemyLiveTriggerController : MonoBehaviour
    {
        [SerializeField] private EnemyFacade _facade;
        [SerializeField] private TriggerObserver _triggerObserver;
    }
}