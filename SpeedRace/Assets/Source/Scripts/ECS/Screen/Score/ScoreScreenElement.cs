using UnityEngine;
using UnityEngine.UI;
using UnityTools.UI;

public class ScoreScreenElement : MonoBehaviour
{
    [SerializeField] private Image _progress;
    [SerializeField] private Image _icon;
    [SerializeField] private ProgressBar _progressBar;

    private CarComponent _car;

    public CarComponent car => _car;

    private float _totalDistance;

    public float progress => _car.movementProgress / _totalDistance;

    public void Init(CarComponent car, float totalDistance)
    {
        _car = car;
        _totalDistance = totalDistance;
        _car.Changed.AddListener(Redraw);
        Redraw();
    }

    public void Dispose()
    {
        _car.Changed.RemoveListener(Redraw);
        _car = null;
    }

    private void Redraw()
    {
        _progressBar.SetProgress(progress);
        _icon.sprite = _car.definition.memeberSprite;
        _progress.color = _car.definition.color;
    }
}