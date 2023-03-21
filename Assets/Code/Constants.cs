
namespace Code
{
    public static class Constants
    {
        public const float epsilon = 0.001f;
        public const float distance = 1f;


        public enum Scenes
        {
            Initial,
            Home,
            EnterMetaPark
        }
        
        #region Tag
        public const string InitialPointTag = "InitialPoint";
        public const string PlayerTag = "Player";
        #endregion

        #region Layers
        public const string HittableLayer = "Hittable";
        public const string PlayerLayer = "Player";
        public const string EnemyLayer = "Enemy";
        #endregion
        
    }
}
