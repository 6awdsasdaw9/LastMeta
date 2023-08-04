using System;
using Code.Logic.Items;
using Code.Services.Shops;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Data.Configs
{
    [CreateAssetMenu(fileName = "ShopsConfig", menuName = "ScriptableObjects/GameData/ShopsConfig")]
    public class ShopsConfig: ScriptableObject
    {
        public ShopData[] ShopsData = new ShopData[1];
    }


    [Serializable]
    public class ShopData
    {
        public ShopType ShopType;
        public SlotData[] Slots = new SlotData[1];
    }
    
    [Serializable]
    public class SlotData
    {
        public ItemType ItemType;
        public int Price;
        [Range(1,3)]public float PriceMultiplayer = 1;
        public bool IsCanSaleForCoupon;
        
        public bool IsPurchase;
        [ShowIf(nameof(IsPurchase)), Range(0,1)] 
        public float PurchasePriceMultiplayer = 0.5f;
    }
}