
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
        public const string StopCameraLayer = "StopCamera";
        #endregion

        #region Sprite Layer Position

        //BackGround
        public const float minusNineLayer = 0.9f;
        public const float minusEightLayer = 0.8f;
        public const float minusSevenLayer = 0.7f;
        //Ground
        public const float minusSixLayer = 0.6f;
        //Fance back
        public const float minusFiveLayer = 0.5f;
        //Decor Back
        public const float minusOneLayer = 0.1f;
        //Decor Front
        public const float oneLayer = -0.1f;
        public const float twoLayer = -0.2f;
        //Fance Front
        public const float treeLayer = -0.3f;
        #endregion

    }
}
