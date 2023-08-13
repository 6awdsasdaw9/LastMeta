using Code.Services.EventsSubscribes;

namespace Code.Character.Enemies.EnemiesInterfaces
{
    public interface IEnemyStats: IEventsSubscriber
    {
        public bool IsLoockLeft { get;  }
        public bool IsPatroling { get; }
        public bool IsMovingToHero { get; }
        public bool IsAttacking { get; }
        public bool IsMelleAttacking { get; }
        public bool IsRangeAttacing { get; }
        public bool IsBlock { get;  }
        void Block();
        void UnBlock();

        void SetLoockLeft(bool isLoockLeft);
    }
}