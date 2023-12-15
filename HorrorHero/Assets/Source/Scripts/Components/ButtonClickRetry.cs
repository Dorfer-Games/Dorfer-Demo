using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;

public class ButtonClickRetry : MonoBehaviour
{
    public void Click()
    {
        Bootstrap.GameRestart(0);
    }
}
