using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PressInsication : GameSystem, IUpdating, IIniting
{
    [SerializeField] private Scrollbar scroll;

    private float currentValue;

    void IIniting.OnInit()
    {
        currentValue = scroll.value;
        Time.timeScale = 1;
        game.fear = false;
    }
    
    void IUpdating.OnUpdate()
    {
        /**/if (!currentValue.Equals(scroll.value) || (Input.GetMouseButtonDown(0) && Input.mousePosition.x > 200))
        {
            Bootstrap.ChangeGameState(EGamestate.Game);
        }
    }
}
