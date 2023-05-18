using System;
using System.Collections.Generic;
using Code.Debugers;
using Cysharp.Threading.Tasks;
using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

namespace Code.UI.Windows.DialogueWindows
{
    public class DialogueController : MonoBehaviour
    {
        private TextAsset _inkJSON = null;
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
            RemoveAllChildrenOfChoises();

            while (_story.canContinue)
            {
                RemoveChildrenOfMessagesOnIndexZero();
                //------------------------------------
                CreateMessage();
                _dialogueText = _story.Continue();
                HandleTags(_story.currentTags);

                await UniTask.Delay(TimeSpan.FromSeconds(_freezeTime));

                foreach (Char letter in _dialogueText.ToCharArray())
                {
                    _dialogueStoryText.text += letter;
                    await UniTask.Delay(TimeSpan.FromSeconds(_typingSpeed));
                    _typingAudioEvent.PlayAudioEvent();
                }
            }


            if (_story.currentChoices.Count > 0)
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
            else
            {
            
                await UniTask.Delay(TimeSpan.FromSeconds(_freezeTime * 5));
                //TODO stop dialogue and close window
                //Close();
            }
        }


        public void DeleteOldStory()
        {
            for (int i = 0; i < _canvasText.childCount; i++)
            {
                Destroy(_canvasText.GetChild(i).gameObject);
            }
        }

        public void SkipStory()
        {
                //TODO stop dialogue and close window
            /*IssueAQuest();
            EventManager.FinishDialogue(_inkJSON);
            Close();*/
        }

        #region MESSAGE
        private void RemoveChildrenOfMessagesOnIndexZero()
        {
            if (!_deleteZeroMessage || _canvasText.childCount < 3)
                return;
            
            _canvasText.GetChild(0).SetAsLastSibling();
            Destroy(_canvasText.GetChild(_canvasText.childCount - 1).gameObject);
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


        private void HandleTags(List<string> currentTags)
        {
            foreach (string tag in currentTags)
            {
                string[] splitTag = tag.Split(':');
                if (splitTag.Length != 2)
                {
                    Debug.LogError("Tag could not be appropriately parsed: " + tag);
                }

                string tagKey = splitTag[0].Trim();
                string tagValue = splitTag[1].Trim();
                switch (tagKey)
                {
                    case SPEAKER_TAG:
                        Image imageMessage = _dialogueMessage.GetComponentInChildren<Image>();

                        if (tagValue == "Lola")
                        {
                            imageMessage.transform.rotation = Quaternion.Euler(0, 180, 0);
                            imageMessage.color = new Color32(177, 211, 255, 255);
                        }
                        else if (tagValue == "01")
                        {
                            imageMessage.color = new Color32(215, 255, 226, 255);
                        }

                        break;
                    default:
                        Debug.LogWarning("Tag came but is not currently being handled: " + tag);
                        break;
                }
            }
        }

   
        private void CreateMessage()
        {
            _dialogueMessage = Instantiate(_messagePrefab);
            _dialogueMessage.transform.SetParent(_canvasText.transform, false);
            _dialogueStoryText = _dialogueMessage.GetComponentInChildren<Text>();
            _dialogueStoryText.text = "";
        }

        #endregion

        #region CHOISE
        
        Button CreateChoiceView(string text)
        {
            // Creates the button from a prefab
            Button choice = Instantiate(_buttonPrefab) as Button;
            choice.transform.SetParent(_canvasButton, false);

            // Gets the text from the button prefab
            choice.GetComponentInChildren<TextMeshProUGUI>().SetText(text);

            return choice;
        }

        void RemoveAllChildrenOfChoises()
        {
            int childButtonCount = _canvasButton.childCount;
            for (int i = childButtonCount - 1; i >= 0; i--)
            {
                Destroy(_canvasButton.GetChild(i).gameObject);
            }
        }

        #endregion
    }
}