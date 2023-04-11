using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Data.Configs
{
    [CreateAssetMenu(fileName = "TextConfig", menuName = "ScriptableObjects/GameData/TextConfig")]
    public class TextConfig : ScriptableObject
    {
        public string DialogueErrorMessage = "Lost internet connection";
        public List<NoteData> Notes;
    } 
    
    [Serializable]
    public class TextData
    {
        public int Id;
        public TextAsset inkJSON;
    }

    [Serializable]
    public class NoteData : TextData
    {
        public Sprite NoteImage;
    }
}