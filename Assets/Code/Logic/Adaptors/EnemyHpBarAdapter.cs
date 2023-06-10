namespace Code.UI.Adaptors
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