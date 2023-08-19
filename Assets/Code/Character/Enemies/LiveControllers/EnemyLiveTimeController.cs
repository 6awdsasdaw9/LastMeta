using System;
using Code.Character.Enemies.EnemiesFacades;
using Code.Debugers;
using Code.Logic.TimingObjects.TimeObserverses;
using Code.Services.EventsSubscribes;
using UnityEngine;

namespace Code.Character.Enemies
{
    public class EnemyLiveTimeController : TimeObserver
    {
        [SerializeField] private EnemyFacade _facade;


        protected override void StartReaction()
        {
            Logg.ColorLog($"{gameObject.name} StartReaction");
            _facade.Revival();
        }


        protected override void EndReaction()
        {
            if (_facade.Health.Current <= 0)
            {
                return;
            }
            _facade.Die();
        }

        protected override void SetStartReaction()
        {
            _facade.Revival();
        }

        protected override void SetEndReaction()
        {
            Logg.ColorLog($"{gameObject.name} SetEndReaction");
            _facade.Animator.PlayDisable();
            _facade.CollisionsController.SetActive(false);
        }

        private void OnValidate()
        {
            _facade = GetComponent<EnemyFacade>();
        }
    }
}