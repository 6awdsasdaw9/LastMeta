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
        private CancellationTokenSource _dialogueCancellationToken;

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
            Logger.ColorLog($"1.DialogueController: StartDialogue", ColorType.Orange);
            _inkJSON = story;
            _story = new Story(_inkJSON.text);

            Logger.ColorLog($"1.DialogueController: StartDialogue -> Clear Button Choices", ColorType.Orange);
            _choiceButtonCreator.ClearButtonChoices();

            OnStartDialogue?.Invoke();
            Logger.ColorLog($"1.DialogueController: StartDialogue -> StartDialogueCircle().Forget()!!!!", ColorType.Orange);
            StartDialogueCircle().Forget();
        }

        private async UniTaskVoid StartDialogueCircle()
        {
            Logger.ColorLog($"1.DialogueController: StartDialogueCircle ? story  == null {_story == null}", ColorType.Orange);
            if (_story == null)
                return;

            Logger.ColorLog($"1.DialogueController:  " +
                            $"story can continue {_story.canContinue}  " +
                            $"_story.currentChoices.Count {_story.currentChoices.Count}", ColorType.Orange);
        
            _dialogueCancellationToken?.Cancel();
            _dialogueCancellationToken = new CancellationTokenSource();
            

            while (_story.canContinue)
            {
                Logger.ColorLog($"2.DialogueController: StartDialogueCircle -> " +
                                $"story can continue {_story.canContinue}", ColorType.Orange);
                
                await _messageBoxCreator.WriteMessage(_story);

                Logger.ColorLog($"3.DialogueController: StartDialogueCircle -> " +
                                $"currentChoices.Count {_story.currentChoices.Count}", ColorType.Orange);
            }

            await UniTask.WaitUntil(() => !_messageBoxCreator.IsTyping, cancellationToken: _dialogueCancellationToken.Token);

            if (_story.currentChoices.Count > 0)
            {
                Logger.ColorLog($"4.DialogueController: StartDialogueCircle -> " +
                                $"Create Choice", ColorType.Orange);
                _choiceButtonCreator.CreateChoice(_story);
            }
            else if (!_story.canContinue)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_params.FreezeTime * 5),
                    cancellationToken: _dialogueCancellationToken.Token);

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

            Logger.ColorLog($"4.DialogueController: Stop Dialogue", ColorType.Orange);
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