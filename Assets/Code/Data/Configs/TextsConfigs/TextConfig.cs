using System;
using System.Collections.Generic;
using Code.Data.Configs.HeroConfigs;
using Code.Logic.Items;
using UnityEngine;

namespace Code.Data.Configs.TextsConfigs
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
        public string NameHeroWindow;
        public ArtifactDescriptionText[] ArtifactsDescriptionText;
        public HeroParamText[]  HeroParamsText;
    }


    [Serializable]
    public class ArtifactDescriptionText
    {
        public ItemType Type;
        public string Title;
        public string Description;
    }

    [Serializable]
    public class HeroParamText
    {
        public HeroUpgradeParamType upgradeParamType;
        public string Title;
    }
}