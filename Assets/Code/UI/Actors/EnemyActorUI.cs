namespace Code.UI.Actors
{
    public class EnemyActorUI : ActorUI
    {
        protected new void Start()
        {
            _hpBar = GetComponentInChildren<HpBar>();
            base.Start();
        }
    }
}