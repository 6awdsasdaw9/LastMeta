namespace Code.Character.Hero.Abilities
{
    public abstract class Ability
    {
        public HeroAbilityType Type { get; protected set; }
        public bool IsOpen { get; protected set; }
        
        public int Level { get; protected set; }
        public float CooldownTime { get; protected set; }


        public abstract void StartApplying();
        public abstract void StopApplying();
    }

   

    public enum HeroAbilityType
    {
        Dash,
        Hand,
        Gun,
        BlackMode,
    }
}