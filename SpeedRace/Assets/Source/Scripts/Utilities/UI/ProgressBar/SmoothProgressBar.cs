using DG.Tweening;
using UnityEngine;

namespace UnityTools.UI
{
    public class SmoothProgressBar : ProgressBar
    {
        [SerializeField] private float _duration;
        [SerializeField] private ProgressBar _target;

        private Tween _currentTween;

        private float _progress;
        private float progress
        {
            get => _progress;
            set
            {
                _progress = value;
                _target.SetProgress(_progress);
            }
        }

        public override void SetProgress(float value)
        {
            if (_currentTween != null) _currentTween.Kill();
            _currentTween = DOTween.To(() => progress, (x) => progress = x, value, _duration);
        }
    }
}