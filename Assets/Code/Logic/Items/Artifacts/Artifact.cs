using System;
using Code.Character.Hero;
using Sirenix.OdinInspector;

namespace Code.Logic.Artifacts.Artifacts
{
    [Serializable]
    public class Artifact
    {
        public ArtifactType Type;
        
        public string Name;
        public string Description;
        public int Level;

        public bool IsActiveAbility;
        [ShowIf(nameof(IsActiveAbility))] public HeroAbilityType AbilityType;

    }
}