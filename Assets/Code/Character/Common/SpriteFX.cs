using System.Threading.Tasks;
using UnityEngine;

namespace Code.Character.Common
{
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
}
