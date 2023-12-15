using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Kuhpik;
using UnityEngine;

public class FearController : GameSystem, IIniting, IUpdating
{
    [SerializeField] private float fearCount;
    [SerializeField] private float braveCount;

    private bool up;
    private GameObject controllValue;
    private Vector3 Position;

    void IIniting.OnInit()
    {
        controllValue = new GameObject();
        Position = Vector3.zero;
        controllValue.transform.position = Position;
        up = true;
    }

    void IUpdating.OnUpdate()
    {
        if (game.fear && up)
        {
            controllValue.transform.DOMoveY(controllValue.transform.position.y + fearCount, 0.2f).SetEase(Ease.OutBounce);
            up = false;
        }
        else
        {
            if (!game.fear)
            {
                up = true;
                if (controllValue.transform.position.y > 0)
                {
                    Position = controllValue.transform.position;
                    Position.y -= braveCount;
                    controllValue.transform.position = Position;
                }
            }
        }

        Position = controllValue.transform.position;
        game.fearBar.value = Position.y;
    }
}
