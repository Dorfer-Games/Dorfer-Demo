using Kuhpik;
using UnityTools.Extentions;

public class CarSelectionSystem : GameSystem
{
    public override void OnStateEnter()
    {
        game.level.ShowStartupCamera();
        for (int i = 0; i < config.carCount; i++)
        {
            SpawnCar(i);
        }
        ChangeGameState(GameStateID.Race);
    }

    private void SpawnCar(int index)
    {
        var definition = config.cars[index];
        var carInstance = Instantiate(definition.prefab, game.level.transform);
        carInstance.index = index;
        carInstance.definition = definition;
        var point = game.level.GetStartPoint(carInstance);
        carInstance.transform.ApplyPoint(point);
        game.cars.Add(carInstance);
    }
}

