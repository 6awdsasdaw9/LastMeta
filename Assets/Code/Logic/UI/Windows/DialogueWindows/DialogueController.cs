using System;
using System.Threading;
using Code.Data.Configs;
using Code.Debugers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Ink.Runtime;
using Zenject;

namespace Code.UI.Windows.DialogueWindows
{
    public class DialogueController : MonoBehaviour
    {
        public DialogueChoiceButtonCreator choiceButtonCreator => _choiceButtonCreator;
        [SerializeField] private DialogueChoiceButtonCreator _choiceButtonCreator;
        public DialogueMessageBoxCreator MessageBoxCreator => _messageBoxCreator;
        [SerializeField] private DialogueMessageBoxCreator _messageBoxCreator;

        private DialogueParams _params;
        private CancellationTokenSource _writeTokenSource;

        private TextAsset _inkJSON;
        private Story _story;
        private string _dialogueText;

        private bool _test;
        [Inject]
        private void Construct(HudSettings hudSettings)
        {
            _params = hudSettings.DialogueParams;
            _choiceButtonCreator.Init(_params);
            _messageBoxCreator.Init(_params);
            _messageBoxCreator.OnWriteMessage += () => StartDialogueStep().Forget();
        }

        private void OnDestroy()
        {
            _messageBoxCreator.OnWriteMessage -= () => StartDialogueStep().Forget();
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

            if(_test)
                return;
            _test = true;
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
            _test = false;
            
        }

        public void StopDialogue()
        {
            _writeTokenSource.Cancel();
            _messageBoxCreator.ClearAllMessage();
            Log.ColorLog("Stop Dialogue", ColorType.Aqua);
            
        }
    }
}