using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;

public class InputSystem : GameSystem, IUpdating
{
    void IUpdating.OnUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            game.masking = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            game.masking = false;
        }
    }
}
