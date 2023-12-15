using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class BoostEffect
{
    public readonly UnityEvent Changed = new UnityEvent();

    public float speedBoost { get; private set; }

    public bool gift { get; private set; }

    private float _durationLeft;
    public float durationLeft
    {
        get => _durationLeft;
        private set
        {
            _durationLeft = Mathf.Max(value, 0);
            Changed?.Invoke();
        }
    }

    public bool expired => _durationLeft <= 0;

    public BoostEffect(float speedBoost, float durationLeft, bool gift)
    {
        this.speedBoost = speedBoost;
        this.durationLeft = durationLeft;
        this.gift = gift;
    }

    public void Update(float deltaTime)
    {
        durationLeft -= deltaTime;
    }
}
