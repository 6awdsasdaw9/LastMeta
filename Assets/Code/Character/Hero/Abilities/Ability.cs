namespace Code.Character.Hero.Abilities
{
    public abstract class Ability
    {
        protected HeroAbilityType Type { get; set; }
        protected bool IsOpen => Level >= 0;
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