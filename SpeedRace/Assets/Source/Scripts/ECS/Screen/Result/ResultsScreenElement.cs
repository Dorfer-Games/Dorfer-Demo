using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultsScreenElement : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _points;

    [SerializeField] private Image _background;

    public void Redraw(CarComponent car, float raceLength)
    {
        _background.color = car.definition.color;
        _name.text = car.definition.displayName;
        _points.text = ((int)(car.movementProgress * raceLength)).ToString();
    }
}