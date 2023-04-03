using UnityEngine;

namespace Code.Data.GameData
{
    [CreateAssetMenu(fileName = "TextData", menuName = "ScriptableObjects/GameData/TextData")]
    public class TextData : ScriptableObject
    {
        public string DialogueErrorMessage = "Lost internet connection";
    } 
}