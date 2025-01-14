using System.Collections.Generic;
using System.Linq;
using Code.Data.Configs;
using Code.Logic.Collisions.Triggers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Debugers
{
    public class DrawerSpawnPoints : MonoBehaviour
    {
        [SerializeField] private ScenesConfig _config;


        [ShowInInspector]
        private List<PointData> points => _config?.ScenesParams
            ?.FirstOrDefault(p => p.Scene.ToString() == SceneManager.GetActiveScene().name)?.Points;

        private void OnDrawGizmos()
        {
            if (_config == null)
                return;

            Gizmos.color = Color.cyan;

            var sceneParams =
                _config.ScenesParams.FirstOrDefault(p => p.Scene.ToString() == SceneManager.GetActiveScene().name);
            if (sceneParams == null)
            {
                Logg.ColorLog("Chesk scene param in ScenesConfig", LogStyle.Warning);
                return;
            }

            var scenePoints = sceneParams.Points;
            for (var index = 0; index < scenePoints.Count; index++)
            {
                var scenePoint = scenePoints[index];
                Gizmos.DrawSphere(scenePoint.Position, index * 0.05f);
            }
        }

        [Button]
        private void SetPoints()
        {
            var sceneParams =
                _config.ScenesParams.FirstOrDefault(p => p.Scene.ToString() == SceneManager.GetActiveScene().name);
            if (sceneParams == null)
            {
                Logg.ColorLog("Chesk scene param in ScenesConfig", LogStyle.Warning);
                return;
            }

            sceneParams.Points.Clear();
            var saveTriggers = FindObjectsOfType<SaveTrigger>().ToList();
            saveTriggers.Sort(new SaveTriggerComparer());
            for (var index = 0; index < saveTriggers.Count; index++)
            {
                var trigger = saveTriggers[index];
                trigger.ID = index;
                sceneParams.Points.Add(trigger.TriggerPointData);
            }
        }
    }

    public class SaveTriggerComparer : IComparer<SaveTrigger>
    {
        public int Compare(SaveTrigger x, SaveTrigger y)
        {
            var xName = x.gameObject.name;
            var yName = y.gameObject.name;

            // Получаем номера объектов из их имен
            var xNumber = int.Parse(xName.Substring(xName.LastIndexOf('_') + 1));
            var yNumber = int.Parse(yName.Substring(yName.LastIndexOf('_') + 1));

            // Сравниваем номера объектов
            return xNumber.CompareTo(yNumber);
        }
    }
}