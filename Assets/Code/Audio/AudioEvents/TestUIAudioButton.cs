using System;
using UnityEngine;
using UnityEngine.UI;


namespace Code.Audio
{
    public class TestUIAudioButton : MonoBehaviour
    {
        [SerializeField] private AudioEvent _uiAudio;


        private void Awake()
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(PlayAudio);
        }

        public void PlayAudio()
        {
            _uiAudio.PlayAudioEvent();
        }
    }
}