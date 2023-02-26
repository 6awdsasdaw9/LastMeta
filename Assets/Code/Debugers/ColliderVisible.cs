using UnityEngine;
using UnityEngine.ProBuilder;

namespace Code.Debugers
{
    public class ColliderVisible : MonoBehaviour
    {
        [SerializeField] private ColliderType type;
        [SerializeField] private bool isVisible;
        [SerializeField] private byte alpha = 130;

        [SerializeField] private ProBuilderMesh mesh;
        private void OnDrawGizmos()
        {
            if(!isVisible)return;
            Gizmos.color = GetColor();
            Gizmos.DrawCube(transform.position, transform.localScale);
        }

        private Color32 GetColor()
        {
            switch (type)
            {
                case ColliderType.Ground:
                    return new Color32(30, 200, 30, alpha);
                case ColliderType.Wall:
                    return new Color32(255, 255, 0, alpha);
            }
            return new Color32(0, 0, 0, 0);
        }
        
        private enum ColliderType
        {
            Ground,
            Wall
        }
    }
}