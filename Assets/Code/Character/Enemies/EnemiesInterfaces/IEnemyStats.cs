namespace Code.Character.Enemies.EnemiesInterfaces
{
    public interface IEnemyStats
    {
        public bool IsPatroling { get; }
        public bool IsMovingToHero { get; }
        public bool IsAttacking { get; }
        public bool IsMelleAttacking { get; }
        public bool IsRangeAttacing { get; }
        public bool IsBlock { get;  }
        void Block();
        void UnBlock();
    }
}