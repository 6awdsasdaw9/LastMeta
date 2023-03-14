namespace Code.Data.ProgressData
{
    public interface ISavedData
    {
        void LoadData(SavedData savedData);

        void SaveData(SavedData savedData);
    }
}
