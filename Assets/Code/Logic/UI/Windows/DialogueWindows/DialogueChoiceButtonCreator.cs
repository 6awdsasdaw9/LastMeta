using System;
using Code.Data.Configs;
using Code.Logic.UI.Windows.DialogueWindows;
using Ink.Runtime;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.UI.Windows.DialogueWindows
{
    [Serializable]
    public class DialogueChoiceButtonCreator
    {
        [SerializeField] private DialogueController _dialogueController;
        [SerializeField] private Transform _canvasButton;

        private ChoiceButton _buttonPrefab;
        private AudioEvent _choiceAudioEvent;
        public bool IsAwaitAnswer { get; private set; }

        public void Init(DialogueParams dialogueParams)
        {
            _buttonPrefab = dialogueParams.ChoiceButtonPrefab;
            _choiceAudioEvent = dialogueParams.ChoiceAudioEvent;
        }

        public void CreateChoice(Story story)
        {
            if (IsAwaitAnswer)
                return;

            IsAwaitAnswer = true;
            foreach (var choice in story.currentChoices)
            {
                ChoiceButton button = CreateChoiceButton(choice.text.Trim());

                button.OnStartTap += delegate
                {
                    _dialogueController.MessageBoxCreator.CreatePlayersAnswer(story, choice);
                    _choiceAudioEvent.PlayAudioEvent();
                    IsAwaitAnswer = false;
                    ClearButtonChoices();
                };
            }
        }

        private ChoiceButton CreateChoiceButton(string text)
        {
            ChoiceButton choice = Object.Instantiate(_buttonPrefab, _canvasButton, false);
            choice.SetText(text);
            return choice;
        }

        public void ClearButtonChoices()
        {
            var childButtonCount = _canvasButton.childCount - 1;
            for (var i = childButtonCount; i >= 0; i--)
            {
                Object.Destroy(_canvasButton.GetChild(i).gameObject);
            }
        }
    }
}