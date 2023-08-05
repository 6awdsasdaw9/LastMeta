using System;
using Code.Audio.AudioEvents;
using Code.UI.HeadUpDisplay;
using Code.UI.HeadUpDisplay.Windows.InteractiveWindows.DialogueWindows;
using FMODUnity;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Data.Configs
{
    [CreateAssetMenu(fileName = "HudSettings", menuName = "ScriptableObjects/GameData/HudSettings")]
    public class HudSettings : ScriptableObject
    {
        public HudFacade realHudFacade;
        public HudFacade gameHudFacade;
        [Space,GUIColor(0.85f, 0.74f, 1)]
        public EventReference ButtonAudioEvent;
   
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
        public ChoiceDefaultButton choiceDefaultButtonPrefab;
    }
}
