using Kuhpik;
using System.Collections;
using System.Linq;
using UnityEngine;

public class ResultsSystem : GameSystemWithScreen<ResultsScreen>
{
    public override void OnStateEnter()
    {
        screen.Init(game.cars, game.level.splineLength);
        game.level.ShowStartupCamera();
        StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        for (int i = 10 - 1; i >= 0; i--)
        {
            screen.SetDurationLeft(i);
            yield return new WaitForSeconds(1);
        }
        ChangeGameState(GameStateID.Loading);
    }
}
