using UnityEngine;
using UnityEngine.UI;

namespace Code.Audio.AudioEvents
{
    public class TestUIAudioButton : MonoBehaviour
    {
        [SerializeField] private AudioEvent _uiAudio;


        private void Awake()
        {
            Button button = GetComponent<Button>();
            if(button == null)
                return;
            button.onClick.AddListener(PlayAudio);
        }

        public void PlayAudio()
        {
            _uiAudio.PlayAudioEvent();
        }
    }
}