using UnityEngine;

namespace Code.Debugers
{
    public class ColliderVisible : MonoBehaviour
    {
#if UNITY_EDITOR
        

        [SerializeField] private BoxCollider _collider;
        [SerializeField] private ColliderType type;
        [SerializeField] private bool isVisible;
        [SerializeField] private byte alpha = 130;

        private void OnDrawGizmos()
        {
            if (!isVisible) return;
            Gizmos.color = GetColor();
            if (_collider)
            {
                Gizmos.DrawCube(transform.position + _collider.center, _collider.size);
            }
            else
            {
                Gizmos.DrawCube(transform.position, transform.localScale);
            }
        }

        private Color32 GetColor()
        {
            return type switch
            {
                ColliderType.Ground => new Color32(30, 200, 30, alpha),
                ColliderType.Wall => new Color32(255, 255, 0, alpha),
                ColliderType.SaveTrigger => new Color32(0, 255, 255, alpha),
                ColliderType.LevelTransferTrigger => new Color32(100, 100, 255, alpha),
                ColliderType.Obstruction => new Color32(255, 0, 150, alpha),
                ColliderType.InteractableObject => new Color32(100, 150, 200, alpha),
                ColliderType.StopCamera => new Color32(0, 100, 100, alpha),
                ColliderType.Joint => new Color32(255, 100, 255, alpha),
                _ => new Color32(0, 0, 0, 0)
            };
        }

        private enum ColliderType
        {
            Ground,
            Wall,
            SaveTrigger,
            LevelTransferTrigger,
            Obstruction,
            InteractableObject,
            StopCamera,
            Joint
        }
#endif
    }
}