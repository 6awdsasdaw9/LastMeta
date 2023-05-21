using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Code.Data.Configs;
using Code.Debugers;
using Cysharp.Threading.Tasks;
using Ink.Runtime;
using UnityEditor.Rendering;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.UI.Windows.DialogueWindows
{
    [Serializable]
    public class DialogueMessageBoxCreator
    {
        [SerializeField] private bool _deleteZeroMessage;
        [SerializeField] private Transform _canvasText;
        [SerializeField] private int _maxMessage = 4;
        
        public Action OnWriteMessage;
        
        private DialogueParams _params;
        private AudioEvent _typingAudioEvent;

        private const string SPEAKER_TAG = "speaker";

        public void Init(DialogueParams param)
        {
            _params = param;
            _typingAudioEvent = _params.TypingAudioEvent;
            OnWriteMessage += () => Log.ColorLog("Write Message");
        }

        public async Task WriteMessage(Story story, bool rightRotate)
        {
            if(!story.canContinue)
                return;

            RemoveChildrenOfMessages();
            var cancellationToken = new CancellationTokenSource();
    
            var fullText = story.Continue();
            var currentText = "";
            var messageBox = CreateMessageBox();

            if(rightRotate)
                messageBox.SetRightRotation();
            
            HandleTags(story.currentTags, messageBox);
            
            await UniTask.Delay(TimeSpan.FromSeconds(_params.FreezeTime), cancellationToken: cancellationToken.Token);
            
            foreach (var letter in fullText)
            {
                currentText += letter;
                messageBox.SetText(currentText);
                _typingAudioEvent.PlayAudioEvent();
                await UniTask.Delay(TimeSpan.FromSeconds(_params.TypingSpeed), cancellationToken: cancellationToken.Token);
            }
            
            OnWriteMessage?.Invoke();
            cancellationToken.Cancel();
        }

        public void CreatePlayersAnswer(Story story, Choice choice)
        {
            RemoveChildrenOfMessages();
            story.ChooseChoiceIndex(choice.index);
            
            var messageBox = Object.Instantiate(_params.MessageBoxPrefab, _canvasText.transform, false);
            messageBox.SetText(story.Continue()); 
            
            WriteMessage(story, rightRotate: true);
        }

        public void ClearAllMessage()
        {
            for (int i = 0; i < _canvasText.childCount; i++)
            {
                Object.Destroy(_canvasText.GetChild(i).gameObject);
            }
        }

        public void RemoveChildrenOfMessages()
        {
            if (!_deleteZeroMessage||_canvasText.childCount < _maxMessage)
                return;
            
            _canvasText.GetChild(0).SetAsLastSibling();
            Object.Destroy(_canvasText.GetChild(_canvasText.childCount - 1).gameObject);
        }

        private MessageBox CreateMessageBox()
        {
            var messageBox = Object.Instantiate(_params.MessageBoxPrefab, _canvasText.transform, false);
            messageBox.SetText("");
            return messageBox;
        }
        
        private void HandleTags(List<string> currentTags,MessageBox messageBox)
        {
            foreach (string tag in currentTags)
            {
                var splitTag = tag.Split(':');
                var tagKey = splitTag[0].Trim();
                var tagValue = splitTag[1].Trim();
                switch (tagKey)
                {
                    case SPEAKER_TAG:
                        switch (tagValue)
                        {
                            case "Lola":
                                messageBox.SetRightRotation();
                                messageBox.SetColor(new Color32(177, 211, 255, 255));
                                break;
                            case "01":
                                messageBox.SetColor(new Color32(215, 255, 226, 255));
                                break;
                        }

                        break;
                    default:
                        Debug.LogWarning("Tag came but is not currently being handled: " + tag);
                        break;
                }
            }
        }
        
        
    }
}