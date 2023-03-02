
using Code.Debugers;
using UnityEngine;
using Zenject;

namespace Code.Logic.DayOfTime
{
    public class LightingToggle : MonoBehaviour
    {

        [Inject]
        private void Construct(TimeOfDayController time)
        {
            time.OnMorning += Morning;
            time.OnEvening += Evening;
            time.OnNight += Night;
        }
        
        private void Morning()
        {
            Log.ColorLog("Is Day");
        }

        private void Evening()
        {
            Log.ColorLog("Is Evening");
        }

        private void Night()
        {
            Log.ColorLog("Is Night");
        }
    }
}