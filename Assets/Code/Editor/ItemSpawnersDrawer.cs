using Code.Logic.Objects.Items;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(ItemSpawner))]
    public class ItemSpawnersDrawer : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(ItemSpawner spawner, GizmoType gizmoType)
        {
            Gizmos.color = new Color32(15, 150, 70, 150);
            Gizmos.DrawSphere(spawner.transform.position, 0.15f);
        }
    }
}