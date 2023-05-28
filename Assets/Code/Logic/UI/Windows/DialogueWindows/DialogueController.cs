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
        [SerializeField] private HudButton _buttonClose;
        
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

            if(_isActive)
                return;
            
            OnStartDialogue?.Invoke();
            _isActive = true;
            _writeTokenSource = new CancellationTokenSource();
            
            _choiceButtonCreator.RemoveAllChildrenOfChoices();
            _messageBoxCreator.RemoveChildrenOfMessages();

            await _messageBoxCreator.WriteMessage(_story, rightRotate: false);

            Log.ColorLog($"_story.currentChoices.Count {_story.currentChoices.Count}", ColorType.Aqua);
           
            if (_story.currentChoices.Count > 0)
            {
                Log.ColorLog($"CreateChoice", ColorType.Aqua);
                _choiceButtonCreator.CreateChoice(_story);
                _writeTokenSource.Cancel();
            }
            else
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_params.FreezeTime * 5),
                    cancellationToken: _writeTokenSource.Token);
                StopDialogue();
            }
            
            _isActive = false;
        }

        public void StopDialogue()
        {
            _isActive = false;
            _writeTokenSource.Cancel();
            _messageBoxCreator.ClearAllMessage();
            _choiceButtonCreator.RemoveAllChildrenOfChoices();
            OnStopDialogue?.Invoke();
        }

        private void SkipMessage()
        {
            _isActive = false;
            _writeTokenSource.Cancel();
            _messageBoxCreator.SkipMessage();
        }
        public void SubscribeToEvent(bool flag)
        {
            if (flag)
            {
                _messageBoxCreator.OnWriteMessage += () => StartDialogueStep().Forget();
                /*_buttonClose.OnStartTap += StopDialogue;
                _buttonSkip.OnStartTap += SkipMessage;*/
            }
            else
            {
                _messageBoxCreator.OnWriteMessage -= () => StartDialogueStep().Forget();
                /*_buttonClose.OnStartTap -= StopDialogue;
                _buttonSkip.OnStartTap -=  SkipMessage;*/
            }
        }
    }
}