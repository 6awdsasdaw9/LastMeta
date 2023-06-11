using System;
using System.Threading;
using Code.Data.Configs;
using Code.PresentationModel.Buttons;
using Code.Services;
using Cysharp.Threading.Tasks;
using Ink.Runtime;
using UnityEngine;
using Zenject;

namespace Code.PresentationModel.Windows.DialogueWindows
{
    public class DialogueController : MonoBehaviour, IEventSubscriber
    {
        [SerializeField] private HudButton _buttonSkip;

        public DialogueChoiceButtonCreator ChoiceButtonCreator => _choiceButtonCreator;
        [SerializeField] private DialogueChoiceButtonCreator _choiceButtonCreator;
        public DialogueMessageBoxCreator MessageBoxCreator => _messageBoxCreator;
        [SerializeField] private DialogueMessageBoxCreator _messageBoxCreator;

        private DialogueParams _params;
        private CancellationTokenSource _dialogueCancellationToken;

        private TextAsset _inkJSON;
        private Story _story;
        private string _dialogueText;

        private bool _isActive;

        public Action OnStartDialogue;
        public Action OnStopDialogue;
        public Action OnDialogueIsEnd;

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
            StopDialogue();
            SubscribeToEvent(false);
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
                _messageBoxCreator.OnWriteMessage += () => StartDialogueCircle().Forget();
                _buttonSkip.OnStartTap -= SkipMessage;
            }
        }

        public void StartDialogue(TextAsset story)
        {
            _inkJSON = story;
            _story = new Story(_inkJSON.text);

            _choiceButtonCreator.ClearButtonChoices();

            OnStartDialogue?.Invoke();
            StartDialogueCircle().Forget();
        }

        private async UniTaskVoid StartDialogueCircle()
        {
            if (_story == null)
                return;
            
            _dialogueCancellationToken?.Cancel();
            _dialogueCancellationToken = new CancellationTokenSource();
            
            while (_story.canContinue)
            {
                await _messageBoxCreator.WriteMessage(_story);
            }

            await UniTask.WaitUntil(() => !_messageBoxCreator.IsTyping, cancellationToken: _dialogueCancellationToken.Token);

            if (_story.currentChoices.Count > 0)
            {
                _choiceButtonCreator.CreateChoice(_story);
            }
            else if (!_story.canContinue)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_params.FreezeTime * 5),
                    cancellationToken: _dialogueCancellationToken.Token);

                OnDialogueIsEnd?.Invoke();
                StopDialogue();
            }
        }

        public void StopDialogue()
        {
            _dialogueCancellationToken?.Cancel();
            _messageBoxCreator.SkipMessage();
            
            _messageBoxCreator.ClearAllMessage();
            _choiceButtonCreator.ClearButtonChoices();

            OnStopDialogue?.Invoke();
        }

        private void SkipMessage()
        {
            if (_choiceButtonCreator.IsAwaitAnswer)
                return;

            _isActive = false;
            _messageBoxCreator.SkipMessage();
        }
    }
}