namespace Code.Services.SaveServices
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
