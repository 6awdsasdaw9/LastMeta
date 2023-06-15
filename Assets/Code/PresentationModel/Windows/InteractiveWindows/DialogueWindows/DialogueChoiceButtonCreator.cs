using System;
using Code.Audio.AudioEvents;
using Code.Data.Configs;
using Ink.Runtime;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.PresentationModel.Windows.DialogueWindows
{
    [Serializable]
    public class DialogueChoiceButtonCreator
    {
        [SerializeField] private DialogueController _dialogueController;
        [SerializeField] private Transform _canvasButton;

        private ChoiceDefaultButton _defaultButtonPrefab;
        private AudioEvent _choiceAudioEvent;
        public bool IsAwaitAnswer { get; private set; }

        public void Init(DialogueParams dialogueParams)
        {
            _defaultButtonPrefab = dialogueParams.choiceDefaultButtonPrefab;
            _choiceAudioEvent = dialogueParams.ChoiceAudioEvent;
        }

        public void CreateChoice(Story story)
        {
            if (IsAwaitAnswer)
                return;

            IsAwaitAnswer = true;
            foreach (var choice in story.currentChoices)
            {
                ChoiceDefaultButton defaultButton = CreateChoiceButton(choice.text.Trim());

                defaultButton.OnStartTap += delegate
                {
                    _dialogueController.MessageBoxCreator.CreatePlayersAnswer(story, choice);
                    _choiceAudioEvent.PlayAudioEvent();
                    IsAwaitAnswer = false;
                    ClearButtonChoices();
                };
            }
        }

        private ChoiceDefaultButton CreateChoiceButton(string text)
        {
            ChoiceDefaultButton choiceDefault = Object.Instantiate(_defaultButtonPrefab, _canvasButton, false);
            choiceDefault.SetText(text);
            return choiceDefault;
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