using System;
using Code.Logic.UI.Windows.DialogueWindows;
using Code.UI;
using Code.UI.HeadUpDisplay;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Data.Configs
{
    [CreateAssetMenu(fileName = "HudSettings", menuName = "ScriptableObjects/GameData/HudSettings")]
    public class HudSettings : ScriptableObject
    {
        public Hud RealHUD;
        public Hud GameHUD;
   
        [Space] public InteractiveUIParams InteractiveUIParams;

        [Space] public DialogueParams DialogueParams;
    }
    
    [Serializable]
    public class InteractiveUIParams
    {
        public float InteractiveCooldownTime = 1.1f;
        public Vector3 InteractiveObjectDownPos = Vector3.down * 1600;
        public Vector3 InteractiveObjectCenterPos = Vector3.zero;
        public float InteractiveObjectTimeToHide = 0.35f;
        public float InteractiveObjectTimeToShow = 0.5f;
    }

    [Serializable]
    public class DialogueParams
    {
        [Title("Values")]
        public bool DeleteZeroMessage = true;
        public float FreezeTime = 0.5f;
        public float TypingSpeed = 0.08f;
        [Title("Audio")]
        public AudioEvent TypingAudioEvent;
        public AudioEvent ChoiceAudioEvent;
        [Title("Prefabs")]
        public MessageBox MessageBoxPrefab;
        public ChoiceButton ChoiceButtonPrefab;
    }
}
