using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Code.Debugers;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

namespace Code.UI.Windows.DialogueWindows
{
    public class DialogueController : MonoBehaviour
    {
        private TextAsset _inkJSON;
        private Story _story;

        [SerializeField] private Transform _canvasText;
        [SerializeField] private Transform _canvasButton;

        [Space] [SerializeField] private GameObject _messagePrefab;
        [SerializeField] private Button _buttonPrefab;

        [Space] [SerializeField] private bool _deleteZeroMessage;
        [SerializeField] private float _typingSpeed = 0.08f;
        [SerializeField] private float _freezeTime = 0.5f;

        [Space] 
        [SerializeField] private AudioEvent _typingAudioEvent;
        [SerializeField] private AudioEvent _choiceAudioEvent;

        private CancellationTokenSource _writeTokenSource;
        private GameObject _dialogueMessage;
        private Text _dialogueStoryText;
        private string _dialogueText;

        private const string SPEAKER_TAG = "speaker";

        public bool IsActive { get; }


        public void StartStory(TextAsset story)
        {
            _inkJSON = story;
            _story = new Story(_inkJSON.text);

            AllStepsOfDialog().Forget();
            Log.ColorLog("START STORY");
        }

        private async UniTaskVoid AllStepsOfDialog()
        {
            RemoveAllChildrenOfChoices();

            while (_story.canContinue)
            {
                RemoveChildrenOfMessagesOnIndexZero();
                await WriteMessage();
            }
            if (_story.currentChoices.Count > 0)
            {
                CreateChoice();
            }
            else
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_freezeTime * 5),cancellationToken:_writeTokenSource.Token);
                //TODO stop dialogue and close window
                StopDialogue();
            }
        }
        

        private void StopDialogue()
        {
            for (int i = 0; i < _canvasText.childCount; i++)
            {
                Destroy(_canvasText.GetChild(i).gameObject);
            }
            
        }
        #region MESSAGE

        private void RemoveChildrenOfMessagesOnIndexZero()
        {
            if (!_deleteZeroMessage || _canvasText.childCount < 3)
                return;
            
            _canvasText.GetChild(0).SetAsLastSibling();
            Destroy(_canvasText.GetChild(_canvasText.childCount - 1).gameObject);
        }

        private async Task WriteMessage()
        {
            _dialogueText = _story.Continue();
            CreateMessage();
            HandleTags(_story.currentTags);
            await UniTask.Delay(TimeSpan.FromSeconds(_freezeTime), cancellationToken: _writeTokenSource.Token);

            foreach (var letter in _dialogueText)
            {
                _dialogueStoryText.text += letter;
                _typingAudioEvent.PlayAudioEvent();
                await UniTask.Delay(TimeSpan.FromSeconds(_typingSpeed), cancellationToken: _writeTokenSource.Token);
            }
        }


        private void CreatePlayersAnswer(Choice choice)
        {
            RemoveChildrenOfMessagesOnIndexZero();
            _story.ChooseChoiceIndex(choice.index);
            GameObject message = Instantiate(_messagePrefab);
            message.GetComponentInChildren<Text>().text = _story.Continue();
            message.transform.SetParent(_canvasText.transform, false);
            Image imageMessage = message.GetComponentInChildren<Image>();
            imageMessage.transform.rotation = Quaternion.Euler(0, 180, 0);
            imageMessage.color = new Color32(177, 211, 255, 255);

            AllStepsOfDialog().Forget();
        }


        private void CreateMessage()
        {
            _dialogueMessage = Instantiate(_messagePrefab);
            _dialogueMessage.transform.SetParent(_canvasText.transform, false);
            _dialogueStoryText = _dialogueMessage.GetComponentInChildren<Text>();
            _dialogueStoryText.text = "";
        }

        private void HandleTags(List<string> currentTags)
        {
            foreach (string tag in currentTags)
            {
                var splitTag = tag.Split(':');
                var tagKey = splitTag[0].Trim();
                var tagValue = splitTag[1].Trim();
                switch (tagKey)
                {
                    case SPEAKER_TAG:
                        Image imageMessage = _dialogueMessage.GetComponentInChildren<Image>();
                        switch (tagValue)
                        {
                            case "Lola":
                                imageMessage.transform.rotation = Quaternion.Euler(0, 180, 0);
                                imageMessage.color = new Color32(177, 211, 255, 255);
                                break;
                            case "01":
                                imageMessage.color = new Color32(215, 255, 226, 255);
                                break;
                        }

                        break;
                    default:
                        Debug.LogWarning("Tag came but is not currently being handled: " + tag);
                        break;
                }
            }
        }

        #endregion

        #region CHOISE

        private void RemoveAllChildrenOfChoices()
        {
            var childButtonCount = _canvasButton.childCount;
            for (var i = childButtonCount - 1; i >= 0; i--)
            {
                Destroy(_canvasButton.GetChild(i).gameObject);
            }
        }

        private void CreateChoice()
        {
            foreach (var choice in _story.currentChoices)
            {
                Button button = CreateChoiceView(choice.text.Trim());

                var choice1 = choice;
                button.onClick.AddListener(delegate
                {
                    CreatePlayersAnswer(choice1);
                    _choiceAudioEvent.PlayAudioEvent();
                });
            }
        }

        private Button CreateChoiceView(string text)
        {
            Button choice = Instantiate(_buttonPrefab);
            choice.transform.SetParent(_canvasButton, false);
            choice.GetComponentInChildren<TextMeshProUGUI>().SetText(text);
            return choice;
        }

        #endregion
    }
}