using UnityEngine;

namespace Code
{
    public static class Constants
    {
        public const string SaveProgressFileName = "ProgressData";
        public const float epsilon = 0.001f;
        public const float distance = 1f;

        public enum GameMode
        {
            Real,
            Game
        }
        
        public enum Scenes
        {
            Initial,
            Home,
            EnterMetaPark,
            EnterMetaPark_Room,
            Polygon,
            Polygon_3000
        }
        
        public enum HeroMode
        {
            Default,
            Gun,
            Black,
        }
       
        
        #region Tag
        public const string InitialPointTag = "InitialPoint";
        public const string PlayerTag = "Player";
        #endregion

        #region Layers
        public const string HittableLayer = "Hittable";
        public const string HeroLayer = "Hero";
        public const string EnemyLayer = "Enemy";
        public const string StopCameraLayer = "StopCamera";
        #endregion

        #region Sprite Layer Position
        public const float SecondLayerPosition = -0.2f;
        #endregion
        
        #region Colors
        public static Color DarkRedColor =  new(1, 0.3f, 0.5f, 1);
        public static Color RedColor =  new(1, 0.75f, 0.85f, 1);
        public static Color OrangeColor =  new(1, 0.85f, 0.75f, 1);
        public static Color YellowColor =  new(1, 0.95f, 0.75f, 1);
        public static Color GreenColor =  new(0.8f, 1, 0.75f, 1);
        public static Color BlueColor =  new(0.75f, 0.95f, 1, 1);
        public static Color DarkBlueColor =  new(0.75f, 0.85f, 1, 1);
        public static Color VioletColor =  new(0.85f, 0.74f, 1, 1);
        public static Color[] RainbowColors = { RedColor,OrangeColor,YellowColor,GreenColor,BlueColor,DarkBlueColor,VioletColor};  
        
        
        #endregion
    }
}
