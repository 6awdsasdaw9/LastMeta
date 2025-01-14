using System;
using System.Threading;
using Code.Data.Configs;
using Code.Services;
using Code.Services.EventsSubscribes;
using Code.UI.HeadUpDisplay.Elements.Buttons;
using Cysharp.Threading.Tasks;
using Ink.Runtime;
using UnityEngine;
using Zenject;

namespace Code.UI.HeadUpDisplay.Windows.InteractiveWindows.DialogueWindows
{
    public class DialogueController : MonoBehaviour, IEventsSubscriber
    {
        [SerializeField] private HudButton buttonSkip;

        public DialogueChoiceButtonCreator ChoiceButtonCreator => _choiceButtonCreator;
        [SerializeField] private DialogueChoiceButtonCreator _choiceButtonCreator;
        public DialogueMessageBoxCreator MessageBoxCreator => _messageBoxCreator;
        [SerializeField] private DialogueMessageBoxCreator _messageBoxCreator;

        private DialogueParams _params;
        private CancellationTokenSource _dialogueCancellationToken;

        private TextAsset _inkJSON;
        private Story _story;
        private string _dialogueText;
        
        public Action OnStartDialogue;
        public Action OnStopDialogue;
        public Action OnDialogueIsEnd;

        [Inject]
        private void Construct(HudSettings hudSettings, EventSubsribersStorage eventSubsribersStorage)
        {
            _params = hudSettings.DialogueParams;
            _choiceButtonCreator.Init(_params);
            _messageBoxCreator.Init(_params);
            eventSubsribersStorage.Add(this);
        }

        private void OnEnable()
        {
            SubscribeToEvents(true);
        }

        private void OnDisable()
        {
            StopDialogue();
            SubscribeToEvents(false);
        }

        public void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _messageBoxCreator.OnWriteMessage += () => StartDialogueCircle().Forget();
                buttonSkip.OnStartTap += SkipMessage;
            }
            else
            {
                _messageBoxCreator.OnWriteMessage += () => StartDialogueCircle().Forget();
                buttonSkip.OnStartTap -= SkipMessage;
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

            _messageBoxCreator.SkipMessage();
        }
    }
}