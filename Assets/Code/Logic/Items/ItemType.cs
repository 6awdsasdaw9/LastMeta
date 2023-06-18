using System;

namespace Code.Logic.Artifacts
{
    public enum ArtifactType
    {
        RightSock,
        LeftSock,
        Glove,
        Gun,
        Substance
    }

    public enum ItemType
    {
        Currency,
        Artifact,
        Buff,
        DeBuff
    }
    
    [Serializable]
    public class ItemData
    {
        public ItemType ItemType;
    }
}