using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Debugers
{
    public class SpritesSetterPosition : MonoBehaviour
    {

        /*public List<SpriteRenderer> minusNineLayer;
    public List<SpriteRenderer> minusEightLayer;
    public List<SpriteRenderer> minusSevenLayer;
    public List<SpriteRenderer> minusSixLayer;
    public List<SpriteRenderer> minusFiveLayer;
    public List<SpriteRenderer> minusFourLayer;
    public List<SpriteRenderer> minusThreeLayer;
    public List<SpriteRenderer> minusTwoLayer;
    public List<SpriteRenderer> minusOneLayer;
    public List<SpriteRenderer> oneLayer;
    public List<SpriteRenderer> twoLayer;
    public List<SpriteRenderer> treeLayer;
    public List<SpriteRenderer> fourLayer;
    public List<SpriteRenderer> fiveLayer;
    public List<SpriteRenderer> sixLayer;*/



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
                    _ => t.transform.position
                };
            }
        }

    
        private Vector3 GetVector(Vector3 position, float dis) => 
            new Vector3(position.x, position.y, dis);
    }
}
