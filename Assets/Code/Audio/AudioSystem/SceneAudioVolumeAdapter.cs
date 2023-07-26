using Code.Services;
using Code.Services.SaveServices;
using Zenject;

namespace Code.Audio.AudioSystem
{
    public class SceneAudioVolumeAdapter: IEventSubscriber, ISavedData
    {
        [Inject]
        private void Construct()
        {
            
        }
        public void SubscribeToEvent(bool flag)
        {
            
        }

        public void LoadData(SavedData savedData)
        {
        }

        public void SaveData(SavedData savedData)
        {
        }
    }
}