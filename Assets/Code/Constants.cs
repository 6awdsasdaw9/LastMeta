using UnityEngine;

namespace Code
{
    public static class Constants
    {
        public const string saveProgressFileName = "ProgressData";
        public const float epsilon = 0.001f;
        public const float distance = 1f;

        public enum TypeOfScene
        {
            Real,
            Game
        }
        
        public enum Scenes
        {
            Initial,
            Home,
            EnterMetaPark,
            EnterMetaPark_Room
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
        
        /*//BackGround
        public const float minusElevenLayer = 1.1f ;
        //BackGround Decoration
        public const float minusTenLayer = 1; //inside home
        public const float minusNineLayer = 0.9f; //outside dark home 
        public const float minusEightLayer = 0.8f;//outside light home 
        public const float minusSevenLayer = 0.7f;
        //Ground
        public const float minusSixLayer = 0.6f;
        //Back Fance 
        public const float minusFiveLayer = 0.5f;
        //Back Decoration 
        public const float minusFourLayer = 0.4f;
        public const float minusTreeLayer = 0.3f;
        public const float minusTwoLayer = 0.2f;
        public const float minusOneLayer = 0.1f;*/
        //Front Decor
        public const float oneLayer = -0.1f;
        public const float twoLayer = -0.2f;
        /*//Front Fance 
        public const float treeLayer = -0.3f;
        //Front Decoration
        public const float  fourLayer = -0.4f;
        public const float  fiveLayer = -0.5f;
        public const float  sixLayer = -0.6f;*/
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
