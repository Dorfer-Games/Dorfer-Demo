using System;
using UnityEngine;
using UnityEngine.Events;

namespace UnityTools
{
    [Serializable]
    public class Timer
    {
        [SerializeField] private float _coolDown;

        private float _coolDownLeft;

        public bool isReady => IsReady();
        public float left => _coolDownLeft;
        public float normalized => Normlized();

        public float coolDown => _coolDown;

        public readonly UnityEvent Changed = new UnityEvent();

        public Timer(float coolDown, float coolDownLeft)
        {
            _coolDown = coolDown;
            _coolDownLeft = coolDownLeft;
        }

        public Timer(float coolDown)
        {
            _coolDown = coolDown;
        }

        public Timer(decimal coolDown)
        {
            _coolDown = (float)coolDown;
        }

        public bool IsReady()
        {
            return _coolDownLeft < 0;
        }

        public float Normlized()
        {
            return 1 - (_coolDownLeft / _coolDown);
        }

        public void UpdateTimer()
        {
            _coolDownLeft -= Time.deltaTime;
            Changed?.Invoke();
        }

        public void UpdateTimer(float time)
        {
            _coolDownLeft -= time;
            Changed?.Invoke();
        }

        public void Reset(float newCoolDown)
        {
            _coolDown = newCoolDown;
            Reset();
        }

        public void Reset()
        {
            _coolDownLeft = _coolDown;
            Changed?.Invoke();
        }

        public void End()
        {
            _coolDownLeft = 0;
            Changed?.Invoke();
        }
    }
}