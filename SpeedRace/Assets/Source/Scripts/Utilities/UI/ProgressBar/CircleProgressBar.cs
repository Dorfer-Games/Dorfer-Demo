using UnityEngine;
using UnityEngine.UI;

namespace UnityTools.UI
{
    public class CircleProgressBar : ProgressBar
    {
        [SerializeField] private Image _image;

        public override void SetProgress(float value)
        {
            _image.fillAmount = value;
        }
    }
}