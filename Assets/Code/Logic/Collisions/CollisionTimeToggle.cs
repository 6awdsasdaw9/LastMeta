using Code.Logic.Collisions;
using Code.Logic.TimingObjects.TimeObserverses;
using UnityEngine;

namespace Code.Logic.Toys
{
    public class CollisionTimeToggle : TimeObserver
    {
        [SerializeField] private CollisionObserver _trigger;

        protected override void StartReaction()
        {
            _trigger.gameObject.SetActive(true);
        }

        protected override void EndReaction()
        {
            _trigger.gameObject.SetActive(false);
        }

        protected override void SetStartReaction()
        {
            _trigger.gameObject.SetActive(true);
        }

        protected override void SetEndReaction()
        {
            _trigger.gameObject.SetActive(false);
        }
    }
}