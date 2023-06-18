namespace Code.Character.Hero
{
    public abstract class Ability
    {
        public HeroAbilityType Type { get; protected set; }
        public bool IsOpen { get; protected set; }
        
        public int Level { get; protected set; }
        public float CooldownTime { get; protected set; }

        public abstract void OpenAbility();

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