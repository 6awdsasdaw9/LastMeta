
namespace Code
{
    public static class Constants
    {
        public const float epsilon = 0.001f;
        public const float distance = 1f;

        #region Scenes
        
        public const string initialScene = "Initial";
        public const string homeScene = "Home";
        public const string enterMetaPark = "EnterMetaPark";
      
        public  enum Scenes
        {
            Initial,
            Home,
            EnterMetaPark
        }
        
        #endregion
        
        #region Tag
        public const string InitialPointTag = "InitialPoint";
        public const string PlayerTag = "Player";

        #endregion

        #region Layers

        public const string HittableLayer = "Hittable";

        #endregion

    }
}
