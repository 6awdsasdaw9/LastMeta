using System;
using Code.Data.Configs;
using Code.PresentationModel.Windows.DialogueWindows;
using Code.Services.Input;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.PresentationModel.Windows.LaptopWindows
{
    public class LaptopMessenger : MonoBehaviour
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _buttonSend;
        
        private MessageBox _messageBoxPrefab;
        private InputService _inputService;
        private string _errorMessage;
        
        [Inject]
        private void Construct(InputService inputService,HudSettings hudSettings, TextConfig textConfig)
        {
            _inputService = inputService;
            _messageBoxPrefab = hudSettings.DialogueParams.MessageBoxPrefab;
            _errorMessage = textConfig.DialogueErrorMessage;
            _buttonSend.onClick.AddListener(SendMessage);
        }
        
        private void Update()
        {
            if (_inputField.text == String.Empty)
                return;
            
            if (_inputService.GetEnterPressed()) 
                SendMessage();
        }


        private void SendMessage()
        {
            MessageBox messageBox = Instantiate(_messageBoxPrefab, _scrollRect.content);
            messageBox.SetRightRotation();
            messageBox.SetErrorMessage(_errorMessage);
            _scrollRect.content.sizeDelta += _messageBoxPrefab.size + Vector2.up * 30;
            _scrollRect.normalizedPosition = Vector2.zero;
            _inputField.text = "";
        }
        
    }
}