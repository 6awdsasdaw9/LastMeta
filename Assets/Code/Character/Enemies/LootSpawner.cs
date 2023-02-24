using UnityEngine;

namespace Code.Character.Enemies
{
    public class LootSpawner : MonoBehaviour
    {

        /*
        public EnemyDeath enemyDeath;
        private IGameFactory _factory;
        private int _lootMin;
        private int _lootMax;
        public void Construct(IGameFactory factory,IRandomService random)
        {
            _factory = factory;
        }

        private void Start()
        {
            enemyDeath.Happened += SpawnLoot;
        }

        private void SpawnLoot()
        {
           GameObject loot =  _factory.CreateLoot();
           loot.transform.position = transform.position;
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }
    */
    }
 

    public interface IRandomService
    {
    }
}
