using System;
using System.Collections.Generic;
using System.Threading;
using Code.Audio.AudioEvents;
using Code.Data.Configs;
using Cysharp.Threading.Tasks;
using Ink.Runtime;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.UI.HeadUpDisplay.Windows.InteractiveWindows.DialogueWindows
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
        private string _fullText;
        private string _currentText;
        private MessageBox _currentMessageBox;

        private CancellationTokenSource _cancellationToken;

        public void Init(DialogueParams param)
        {
            _params = param;
            _typingAudioEvent = _params.TypingAudioEvent;
        }

        public async UniTask WriteMessage(Story story)
        {
            if (!story.canContinue)
                return;

            IsTyping = true;
            RemoveExcessMessageBoxes();
            CreateNewMessageBox(story);
            HandleTags(story.currentTags, _currentMessageBox);

            foreach (var letter in _fullText)
            { 
                _cancellationToken?.Cancel();
                _cancellationToken = new CancellationTokenSource();

                _currentText += letter;
                _currentMessageBox.SetText(_currentText);
                _typingAudioEvent.PlayAudioEvent();

                await UniTask.Delay(TimeSpan.FromSeconds(_params.TypingSpeed),
                    cancellationToken: _cancellationToken.Token);
            }

            IsTyping = false;
            OnWriteMessage?.Invoke();
        }

        private void CreateNewMessageBox(Story story)
        {
            _currentMessageBox = CreateBox();
            _fullText = story.Continue();
            _currentText = "";
            _currentMessageBox.SetText(_currentText);
        }

        public void CreatePlayersAnswer(Story story, Choice choice)
        {
            RemoveExcessMessageBoxes();
            story.ChooseAnswerIndex(choice.index);
            CreateNewMessageBox(story);
            SkipMessage();
        }

        private MessageBox CreateBox()
        {
            var messageBox = Object.Instantiate(_params.MessageBoxPrefab, _canvasText.transform, false);
            messageBox.SetText("");
            return messageBox;
        }

        public void SkipMessage()
        { 
            if(_currentMessageBox == null)
                return;

            IsTyping = false;
            
            _cancellationToken?.Cancel();
            _currentMessageBox.SetText(_fullText);
            _typingAudioEvent.PlayAudioEvent();
            OnWriteMessage?.Invoke();
        }

        public void ClearAllMessage()
        {
            for (int i = 0; i < _canvasText.childCount; i++)
            {
                Object.Destroy(_canvasText.GetChild(i).gameObject);
            }
        }

        public void RemoveExcessMessageBoxes()
        {
            if (!_deleteZeroMessage || _canvasText.childCount < _maxMessage)
                return;

            _canvasText.GetChild(0).SetAsLastSibling();
            Object.Destroy(_canvasText.GetChild(_canvasText.childCount - 1).gameObject);
        }

        private void HandleTags(List<string> currentTags, MessageBox messageBox)
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

        public bool IsTyping { get; private set; }
    }
}