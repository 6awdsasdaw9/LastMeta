using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Debugers
{
    public class SpritesSetterPosition : MonoBehaviour
    {
        private const string NotEditTag = "NotEditPosition";
        
        [Button]
        public void SetSpritePos(float distanceBetweenLayer = 0.1f)
        {
            var objects = FindObjectsOfType<Renderer>();
            foreach (var obj in objects)
            {
                if (obj.CompareTag(NotEditTag))
                    continue;
                
                var distance = obj.sortingOrder * distanceBetweenLayer;
                obj.transform.position = GetPositionZ(obj.transform.position, -distance);
            }
        }
        
        private Vector3 GetPositionZ(Vector3 position, float dis) => new(position.x, position.y, dis);
    }
}
