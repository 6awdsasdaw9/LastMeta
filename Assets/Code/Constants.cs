using UnityEngine;


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
        
        #region Colors
        public static Color RedColor =  new Color(1, 0.75f, 0.85f, 1);
        public static Color OrangeColor =  new Color(1, 0.85f, 0.75f, 1);
        public static Color YellowColor =  new Color(1, 0.95f, 0.75f, 1);
        public static Color GreenColor =  new Color(0.8f, 1, 0.75f, 1);
        public static Color BlueColor =  new Color(0.75f, 0.95f, 1, 1);
        public static Color DarkBlueColor =  new Color(0.75f, 0.85f, 1, 1);
        public static Color VioletColor =  new Color(0.85f, 0.74f, 1, 1);
        public static Color[] RainbowColor = new Color[]
            { RedColor,OrangeColor,YellowColor,GreenColor,BlueColor,DarkBlueColor,VioletColor};
        #endregion

    }
}
