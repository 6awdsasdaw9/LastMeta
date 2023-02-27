namespace Code.Data.DataPersistence
{
    public interface IDataPersistence
    {
        void LoadData(ProgressData progressData);

        void SaveData(ProgressData progressData);
    }
}
