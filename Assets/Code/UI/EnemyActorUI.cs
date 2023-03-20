namespace Code.UI
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