using System;
using System.Collections.Generic;
using Code.Logic.Artifacts;
using UnityEngine;

namespace Code.Data.Configs
{
    [CreateAssetMenu(fileName = "TextConfig", menuName = "ScriptableObjects/GameData/TextConfigs/TextConfig")]
    public  class TextConfig : ScriptableObject
    {
        public string DialogueErrorMessage = "Lost internet connection";
        public List<NoteData> Notes;
        public HudNamings HudNamings;
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

    [Serializable]
    public class HudNamings
    {
        public string NameMenuWindow;
        public string NameHeroInformationWindow;
        public ArtifactDescriptionText[] ArtifactsDescriptionText;
        public HeroParamText[]  HeroParamsText;
    }


    [Serializable]
    public class ArtifactDescriptionText
    {
        public ArtifactType Type;
        public string Title;
        public string Description;
    }

    [Serializable]
    public class HeroParamText
    {
        public HeroParamType ParamType;
        public string Title;
    }
}