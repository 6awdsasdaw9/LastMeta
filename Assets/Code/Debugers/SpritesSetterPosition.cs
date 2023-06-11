using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Debugers
{
    public class SpritesSetterPosition : MonoBehaviour
    {
#if UNITY_EDITOR
        
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
                obj.transform.position = GetPosition(obj.transform.position, -distance);
            }
        }
        
        private Vector3 GetPosition(Vector3 position, float distance) => new(position.x, position.y, distance);
        
#endif
    }
}
