namespace Code.Data.ProgressData
{
    public interface ISavedData : ISavedDataReader
    {
        void SaveData(SavedData savedData);
    }

    public interface ISavedDataReader
    {
        void LoadData(SavedData savedData);
    }

}
