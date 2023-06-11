using Code.Infrastructure.StateMachine;
using Code.Infrastructure.StateMachine.States;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Code.PresentationModel.Buttons
{
    public class ButtonRestartScene : MonoBehaviour
    {
    
        [SerializeField] private Button _button;
        private GameStateMachine _stateMachine;

        [Inject]
        private void Construct(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private void Start()
        {
            _button.onClick.AddListener(ReloadScene);
        }
        private void ReloadScene()
        {
            _stateMachine.Enter<LoadLevelState, string>(CurrentLevel());
        }
        private string CurrentLevel() =>
            SceneManager.GetActiveScene().name;
    
    }
}