using UnityEngine;

namespace Code.Debugers
{
    public class Fps : MonoBehaviour
    {
        private const float UpdateInterval = 1.0f;
        private float _timeleft;
        private float _lastTime;
        private float _timeSpan;
        private int _lastFrame;
        private int _frames;
        private float _fps;

        void Update()
        {
            _timeleft += Time.deltaTime;
            if (_timeleft > UpdateInterval)
            {
                _timeleft -= UpdateInterval;
                _frames = Time.frameCount - _lastFrame;
                _lastFrame = Time.frameCount;
                _timeSpan = Time.realtimeSinceStartup - _lastTime;
                _lastTime = Time.realtimeSinceStartup;
                _fps = Mathf.RoundToInt(_frames / _timeSpan);
            }
        }

        void OnGUI()
        {
            GUI.Box(new Rect(10, 10, 70, 25), string.Format("fps {0}", _fps));
        }
    }
}