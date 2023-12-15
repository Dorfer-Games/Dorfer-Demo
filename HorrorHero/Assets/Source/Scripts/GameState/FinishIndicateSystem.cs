using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Kuhpik;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class FinishIndicateSystem : GameSystem, IIniting,IUpdating
{
    private NavMeshAgent victimAgent;
    private EndLevel endlvl;
    void IIniting.OnInit()
    {
        victimAgent = game.victimGhost.GetComponent<NavMeshAgent>();
        endlvl = game.finish.GetComponent<EndLevel>();
    }
    void IUpdating.OnUpdate()
    {
        if (game.seeBackward & game.masking)
        {
            victimAgent.enabled = false;
            Bootstrap.ChangeGameState(EGamestate.Lose);
        }

        if (game.fearBar.value >= 0.95f)
        {
            victimAgent.enabled = false;
            Bootstrap.ChangeGameState(EGamestate.Lose);
        }

        if (endlvl.end)
        {
            victimAgent.enabled = false;
            Bootstrap.ChangeGameState(EGamestate.Win);
        }

        if (game.victim.transform.position.z - game.murder.transform.position.z > 18)
        {
            victimAgent.enabled = false;
            Bootstrap.ChangeGameState(EGamestate.Lose);
        }
        
    }
}
