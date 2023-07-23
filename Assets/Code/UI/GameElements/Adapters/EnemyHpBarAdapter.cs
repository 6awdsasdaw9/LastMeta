namespace Code.UI.GameElements.Adapters
{
    public class EnemyHpBarAdapter : HpBarAdapter
    {
        protected new void Start()
        {
            _hpBar = GetComponentInChildren<HpBar>();
            base.Start();
        }
    }
}