using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Code;
using UnityEngine;

public class SpriteFX : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    public async void RedColorize()
    {
        _spriteRenderer.color = Constants.RedColor;
        await Task.Delay(500);
        _spriteRenderer.color = Color.white;
    }  
}
