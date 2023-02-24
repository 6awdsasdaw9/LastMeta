using UnityEngine;

namespace Code.Debugers
{
    public class ColliderVisible : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color32(30, 200, 30, 130);
            Gizmos.DrawCube(transform.position, transform.localScale);
        }
    }
}