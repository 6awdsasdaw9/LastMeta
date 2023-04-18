using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Debugers
{
    public class SpritesSetterPosition : MonoBehaviour
    {
        [Button]
        public void SetSpriteDistance()
        {
            SpriteRenderer[] test = FindObjectsOfType<SpriteRenderer>();
            foreach (var t in test)
            {
                t.transform.position = t.sortingOrder switch
                {
                    -10 => GetVector(t.transform.position, Constants.minusTenLayer),
                    -9 => GetVector(t.transform.position, Constants.minusNineLayer),
                    -8 => GetVector(t.transform.position, Constants.minusEightLayer),
                    -7 => GetVector(t.transform.position, Constants.minusSevenLayer),
                    -6 => GetVector(t.transform.position, Constants.minusSixLayer),
                    -5 => GetVector(t.transform.position, Constants.minusFiveLayer),
                    -4 => GetVector(t.transform.position, Constants.minusFourLayer),
                    -3 => GetVector(t.transform.position, Constants.minusTreeLayer),
                    -2 => GetVector(t.transform.position, Constants.minusTwoLayer),
                    -1 => GetVector(t.transform.position, Constants.minusOneLayer),
                    1 => GetVector(t.transform.position, Constants.oneLayer),
                    2 => GetVector(t.transform.position, Constants.twoLayer),
                    3 => GetVector(t.transform.position, Constants.treeLayer),
                    4 => GetVector(t.transform.position, Constants.fourLayer),
                    5 => GetVector(t.transform.position, Constants.fiveLayer),
                    6 => GetVector(t.transform.position, Constants.sixLayer),
                    _ => t.transform.position
                };
            }
        }

    
        private Vector3 GetVector(Vector3 position, float dis) => 
            new Vector3(position.x, position.y, dis);
    }
}
