using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarComponent : MonoBehaviour
{
    [field: SerializeField] public ParticleSystem commentBoost { get; private set; }
    [field: SerializeField] public ParticleSystem giftBoost { get; private set; }

    public readonly UnityEvent Changed = new UnityEvent();

    private float _movementProgress;
    public float movementProgress
    {
        get => _movementProgress;
        set
        {
            _movementProgress = value;
            Changed?.Invoke();
        }
    }

    public int index { get; set; }
    public CarDefinition definition { get; set; }

    public bool isStarted { get; set; }

    public List<BoostEffect> effects { get; } = new List<BoostEffect>();

    private void Awake()
    {
        commentBoost.Stop();
        giftBoost.Stop();

        SetGiftBoost(false);
    }

    public void ShowBoost()
    {
        commentBoost.Play();
    }

    public void SetGiftBoost(bool state)
    {
        giftBoost.gameObject.SetActive(state);
    }
}
