using UnityEngine;
using UnityEngine.UI;

namespace UnityTools.UI
{
    public class MaskProgressBar : ProgressBar
    {
        [SerializeField] private Image _image;
        [SerializeField] private Mask _mask;

        private void Awake()
        {
            _image.type = Image.Type.Filled;
        }

        public override void SetProgress(float value)
        {
            _image.fillAmount = value;
        }
    }
}