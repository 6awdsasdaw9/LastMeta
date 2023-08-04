namespace Code.Character.Enemies.EnemiesInterfaces
{
    public interface IEnemyAttack
    {
        void StartAttack();
        void OnAttack();
        void OnAttackEnded();
    }
}