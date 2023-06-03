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
using Logger = Code.Debugers.Logger;

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
            Logger.ColorLog($"1.DialogueController: StartDialogue", ColorType.Orange);
            _inkJSON = story;
            _story = new Story(_inkJSON.text);

            _choiceButtonCreator.ClearButtonChoices();
            
            OnStartDialogue?.Invoke();
            StartDialogueCircle().Forget();
        }

        public void StopDialogue()
        {
            _messageBoxCreator.SkipMessage();

            _messageBoxCreator.ClearAllMessage();
            _choiceButtonCreator.ClearButtonChoices();

            OnStopDialogue?.Invoke();
        }

        private async UniTaskVoid StartDialogueCircle()
        {
            while (_story.canContinue)
            {
                Logger.ColorLog($"2.DialogueController: StartDialogueCircle -> " +
                                $"story can continue {_story.canContinue}", ColorType.Orange);
                _messageBoxCreator.RemoveExcessMessageBoxes();
                await _messageBoxCreator.WriteMessage(_story);

                Logger.ColorLog($"3.DialogueController: StartDialogueCircle -> " +
                                $"currentChoices.Count {_story.currentChoices.Count}", ColorType.Orange);
                
               
            }

            if (_story.currentChoices.Count > 0)
            {
                Logger.ColorLog($"4.DialogueController: StartDialogueCircle -> " +
                                $"Create Choice", ColorType.Orange);
                _choiceButtonCreator.CreateChoice(_story);
            }
     
            else
            if (!_story.canContinue)
            {
                _writeTokenSource?.Cancel();
                _writeTokenSource = new CancellationTokenSource();

                await UniTask.Delay(TimeSpan.FromSeconds(_params.FreezeTime * 5),
                    cancellationToken: _writeTokenSource.Token);

                StopDialogue();
            }
        }

        private void SkipMessage()
        {
            if (_choiceButtonCreator.IsAwaitAnswer)
                return;

            _isActive = false;
            _messageBoxCreator.SkipMessage();

            StartDialogueCircle().Forget();
        }

        public void SubscribeToEvent(bool flag)
        {
            if (flag)
            {
                _messageBoxCreator.OnWriteMessage += () => StartDialogueCircle().Forget();
                _buttonSkip.OnStartTap += SkipMessage;
            }
            else
            {
                _messageBoxCreator.OnWriteMessage -= () => StartDialogueCircle().Forget();
                _buttonSkip.OnStartTap -= SkipMessage;
            }
        }
    }
}