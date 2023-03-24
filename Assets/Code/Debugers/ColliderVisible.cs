using UnityEngine;

namespace Code.Debugers
{
    public class ColliderVisible : MonoBehaviour
    {
        [SerializeField] private ColliderType type;
        [SerializeField] private bool isVisible;
        [SerializeField] private byte alpha = 130;
        
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
                case ColliderType.SaveTrigger:
                    return new Color32(0, 255, 255, alpha);
                case ColliderType.LevelTransferTrigger:
                    return new Color32(100, 100, 255, alpha);
                case ColliderType.Obstruction:
                    return new Color32(255, 0, 150, alpha);
                case ColliderType.StopCamera:
                    return new Color32(0, 100, 100, alpha);
                case ColliderType.Joint:
                    return new Color32(255, 100, 255, alpha);
            }
            return new Color32(0, 0, 0, 0);
        }
        
        private enum ColliderType
        {
            Ground,
            Wall,
            SaveTrigger,
            LevelTransferTrigger,
            Obstruction,
            StopCamera,
            Joint
        }
    }
}