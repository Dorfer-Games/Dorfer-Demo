using Kuhpik;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityTools.Extentions;
using TMPro;

public class ResultsScreen : UIScreen
{
    [SerializeField] private List<ResultsScreenElement> _elements;

    [SerializeField] private TMP_Text _durationLeft;

    public void Init(List<CarComponent> cars, float raceLength)
    {
        cars
            .OrderByDescending(x => x.movementProgress)
            .ToList()
            .ForEach((x, i) => _elements[i].Redraw(x, raceLength));
    }

    public void SetDurationLeft(int duration)
    {
        _durationLeft.text = duration.ToString();
    }
}
