namespace Code.Character.Enemies
{
    public interface IEnemyAttack
    {
        void StartAttack();
        void OnAttack();
        void OnAttackEnded();
    }
}