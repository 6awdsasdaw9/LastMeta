using System.Collections.Generic;
using System.Linq;
using Code.Data.Configs;
using Code.Logic.Triggers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Debugers
{
    public class DrawerSpawnPoints : MonoBehaviour
    {


        [SerializeField] private int _configIndex;
        [SerializeField] private ScenesConfig _config;

        [ShowInInspector]
        private Constants.Scenes _selectedScene => 
               _config == null || _config.ScenesParams.Count <= _configIndex
                ? Constants.Scenes.Initial
                : _config.ScenesParams[_configIndex].Scene;
        
        
        [ShowInInspector]
        private List<PointData> points =>  _config == null || _config.ScenesParams.Count <= _configIndex
            ? null
            : _config.ScenesParams[_configIndex].Points;
        private void OnDrawGizmos()
        {
            if (_config == null)
                return;

            Gizmos.color = Color.cyan;
            var scenePoints = _config.ScenesParams[_configIndex].Points;
            for (var index = 0; index < scenePoints.Count; index++)
            {
                var scenePoint = scenePoints[index];
                Gizmos.DrawSphere(scenePoint.Position, index * 0.05f);
            }
        }

        [Button]
        private void SetPoints()
        {
            _config.ScenesParams[_configIndex].Points.Clear();

            var saveTriggers = FindObjectsOfType<SaveTrigger>().ToList();
            saveTriggers.Sort(new SaveTriggerComparer());
            // Перебираем все объекты на сцене
            for (var index = 0; index < saveTriggers.Count; index++)
            {
                var trigger = saveTriggers[index];
                trigger.ID = index;
                _config.ScenesParams[_configIndex].Points.Add(trigger.TriggerPointData);
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