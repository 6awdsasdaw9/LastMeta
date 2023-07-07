using Code.Debugers;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Code.Services.Input
{
    public class CanvasPressListener: MonoBehaviour, IPointerEnterHandler , IPointerExitHandler
    {
        private InputService _inputService;

        [Inject]
        private void Construct(InputService inputService)
        {
            _inputService = inputService;
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            _inputService.SetPressOnUI(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _inputService.SetPressOnUI(false);
        }
    }
}