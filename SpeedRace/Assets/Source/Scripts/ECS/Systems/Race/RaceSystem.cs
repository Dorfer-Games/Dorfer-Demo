using Kuhpik;
using System.Linq;
using UnityEngine;
using UnityTools.Extentions;

public class RaceSystem : GameSystemWithScreen<ScoreScreen>
{
    public override void OnUpdate()
    {
        screen.UpdateProgress();
        MoveCars();
        UpdateCamera();
        CheckWin();
    }

    private void UpdateCamera()
    {
        if (game.cars.Count(x => x.isStarted) > 0)
        {
            screen.Text.SetActive(false);
            game.level.ShowMainCamera();
        }
    }

    public override void OnStateEnter()
    {
        screen.Init(game.cars, config.targetPointsAmmount);
    }

    public override void OnStateExit()
    {
        screen.Dispose();
    }

    private void MoveCars()
    {
        foreach (var car in game.cars)
        {
            if (!car.isStarted) continue;
            var speed = GetCarSpeed(car) * Time.deltaTime;
            var oldProgresss = car.movementProgress;
            car.movementProgress += speed;
            var point = game.level.GetCarPoint(car);
            car.transform.ApplyPoint(point);
            if (Mathf.FloorToInt(car.movementProgress) > Mathf.FloorToInt(oldProgresss))
            {
                Instantiate(config.loopPointsDisplay, car.transform.position + Vector3.up * 3, Quaternion.identity, game.level.transform);
            }
        }
    }

    private void CheckWin()
    {
        if (game.cars.Any(x => x.movementProgress >= config.targetPointsAmmount))
        {
            ChangeGameState(GameStateID.Results);
        }
    }

    private float GetCarSpeed(CarComponent car)
    {
        var result = config.baseCarSpeed;
        for (int i = 0; i < car.effects.Count; i++)
        {
            var effect = car.effects[i];
            result += effect.speedBoost;
        }
        return result;
    }
}

