using System.Collections;
using System.Collections.Generic;
using Code;
using Sirenix.OdinInspector;
using UnityEngine;

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
            if (t.sortingOrder == -9)
                t.transform.position = GetVector(t.transform.position, Constants.minusNineLayer);
            else if(t.sortingOrder == -8)
                t.transform.position = GetVector(t.transform.position, Constants.minusEightLayer);
            else if(t.sortingOrder == -7)
                t.transform.position = GetVector(t.transform.position, Constants.minusSevenLayer);
            else if(t.sortingOrder == -6)
                t.transform.position = GetVector(t.transform.position, Constants.minusSixLayer);
            else if(t.sortingOrder == -5)
                t.transform.position = GetVector(t.transform.position, Constants.minusFiveLayer);
            else if(t.sortingOrder == -1)
                t.transform.position = GetVector(t.transform.position, Constants.minusOneLayer);
            else if(t.sortingOrder == 1)
                t.transform.position = GetVector(t.transform.position, Constants.oneLayer);
            else if(t.sortingOrder == 2)
                t.transform.position = GetVector(t.transform.position, Constants.twoLayer);
            else if(t.sortingOrder == 3)
                t.transform.position = GetVector(t.transform.position, Constants.treeLayer);

            
        }
     
    }

    
    private Vector3 GetVector(Vector3 position, float dis) => 
        new Vector3(position.x, position.y, dis);
}
