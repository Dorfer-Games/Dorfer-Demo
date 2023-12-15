using Kuhpik;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityTools.Extentions;

public class ScoreScreen : UIScreen
{
    [SerializeField] private List<ScoreScreenElement> _elements;
    [SerializeField] private Image _leader;
    [SerializeField] private GameObject _text;

    public GameObject Text => _text;

    public void Init(List<CarComponent> cars, float totalDistance)
    {
        var sorted = cars.OrderBy(x => x.index).ToList();
        for (int i = 0; i < sorted.Count; i++)
        {
            _elements[i].Init(sorted[i], totalDistance);
        }
    }

    public void UpdateProgress()
    {
        var sorted = _elements
            .OrderByDescending(x => x.progress)
            .ToList();

        sorted.ForEach((x, i) => x.transform.SetSiblingIndex(i));

        _leader.sprite = sorted.FirstOrDefault().car.definition.leaderSprite;
    }

    public void Dispose()
    {
        foreach (var element in _elements)
        {
            element.Dispose();
        }
    }
}
