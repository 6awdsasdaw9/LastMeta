using System;
using System.Threading;
using Code.Data.Configs;
using Code.Debugers;
using Code.Services;
using Code.UI.Buttons;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Ink.Runtime;
using Zenject;

namespace Code.UI.Windows.DialogueWindows
{
    public class DialogueController : MonoBehaviour, IEventSubscriber
    {
        [SerializeField] private HudButton _buttonSkip;

        public DialogueChoiceButtonCreator ChoiceButtonCreator => _choiceButtonCreator;
        [SerializeField] private DialogueChoiceButtonCreator _choiceButtonCreator;
        public DialogueMessageBoxCreator MessageBoxCreator => _messageBoxCreator;
        [SerializeField] private DialogueMessageBoxCreator _messageBoxCreator;

        private DialogueParams _params;
        private CancellationTokenSource _writeTokenSource;

        private TextAsset _inkJSON;
        private Story _story;
        private string _dialogueText;

        private bool _isActive;

        public Action OnStartDialogue;
        public Action OnStopDialogue;

        [Inject]
        private void Construct(HudSettings hudSettings)
        {
            _params = hudSettings.DialogueParams;
            _choiceButtonCreator.Init(_params);
            _messageBoxCreator.Init(_params);
        }

        private void OnEnable()
        {
            SubscribeToEvent(true);
        }

        private void OnDisable()
        {
            SubscribeToEvent(false);
        }

        public void StartDialogue(TextAsset story)
        {
            _inkJSON = story;
            _story = new Story(_inkJSON.text);

            StartDialogueStep().Forget();
            Log.ColorLog("Start Dialogue", ColorType.Aqua);
        }

        private async UniTaskVoid StartDialogueStep()
        {
            Log.ColorLog("AllStepsOfDialogue", ColorType.Aqua);

            if (_isActive)
                return;

            OnStartDialogue?.Invoke();
            _isActive = true;
            
            while (_story.canContinue)
            {
                _choiceButtonCreator.ClearButtonChoices();
                _messageBoxCreator.RemoveExcessMessageBoxes();

                await _messageBoxCreator.WriteMessage(_story);
            }
            
            if (_story.currentChoices.Count > 0)
            {
                Log.ColorLog($"CreateChoice", ColorType.Aqua);
                _choiceButtonCreator.CreateChoice(_story);
            }
            
            else if (!_story.canContinue)
            {
                Log.ColorLog("DIALOGUE FINISH", ColorType.Aqua);

                _writeTokenSource?.Cancel();
                _writeTokenSource = new CancellationTokenSource();

                await UniTask.Delay(TimeSpan.FromSeconds(_params.FreezeTime * 5),
                    cancellationToken: _writeTokenSource.Token);

                StopDialogue();
            }

            _isActive = false;
        }

        public void StopDialogue()
        {
            SkipMessage();

            _messageBoxCreator.ClearAllMessage();
            _choiceButtonCreator.ClearButtonChoices();

            OnStopDialogue?.Invoke();
        }

        private void SkipMessage()
        {
            if(_choiceButtonCreator.IsAwaitAnswer)
                return;
            _isActive = false;
            _messageBoxCreator.SkipMessage();
            StartDialogueStep().Forget();
        }

        public void SubscribeToEvent(bool flag)
        {
            if (flag)
            {
                _messageBoxCreator.OnWriteMessage += () => StartDialogueStep().Forget();
                _buttonSkip.OnStartTap += SkipMessage;
            }
            else
            {
                _messageBoxCreator.OnWriteMessage -= () => StartDialogueStep().Forget();
                _buttonSkip.OnStartTap -= SkipMessage;
            }
        }
    }
}