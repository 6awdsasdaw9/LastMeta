namespace Code.Data.SavedDataPersistence
{
    public interface ISavedData
    {
        void LoadData(SavedData savedData);

        void SaveData(SavedData savedData);
    }
}
