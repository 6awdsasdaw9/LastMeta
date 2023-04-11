using System;
using Code.Data.Configs;
using Code.Services.Input;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Windows
{
    public class LaptopMessenger : MonoBehaviour
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _buttonSend;
        
        private DialogueCloud _dialogueCloudPrefab;
        private InputService _inputService;
        private string _errorMessage;
        
        [Inject]
        private void Construct(InputService inputService,PrefabsData prefabsData, TextConfig textConfig)
        {
            _inputService = inputService;
            _dialogueCloudPrefab = prefabsData.DialogueCloud;
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
            DialogueCloud cloud = Instantiate(_dialogueCloudPrefab, _scrollRect.content);
            cloud.SetRightRotation();
            cloud.SetErrorMessage(_errorMessage);
            _scrollRect.content.sizeDelta += _dialogueCloudPrefab.size + Vector2.up * 30;
            _scrollRect.normalizedPosition = Vector2.zero;
            _inputField.text = "";
        }
        
    }
}