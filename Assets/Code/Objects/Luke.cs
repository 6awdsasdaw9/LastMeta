using Code.Services;
using UnityEngine;

public class Luke : MonoBehaviour
{
    [SerializeField] private TriggerObserver _triggerObserver;
    [SerializeField] private GameObject _groundCollider;

    private void Start()
    {
        _triggerObserver.TriggerEnter += TriggerEnter;
    }

    private void TriggerEnter(Collider obj)
    {
        _groundCollider.SetActive(false);
    }
}