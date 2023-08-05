    using Code.Infrastructure.StateMachine;
using Code.Infrastructure.StateMachine.States;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.UI.HeadUpDisplay.Elements.Buttons
{
    public class ButtonRestartScene : MonoBehaviour
    {
    
        [SerializeField] private UnityEngine.UI.Button _button;
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