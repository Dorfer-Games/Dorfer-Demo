using Kuhpik;
using UnityEngine;
using UnityTools.Extentions;
using Random = System.Random;

public class LoadingSystem : GameSystemWithScreen<ScoreScreen>
{
    private LevelLoadingConfig _config;

    public override void OnStateEnter()
    {
        LoadLevel(GetLevelID(save.levelID));
    }

    private void LoadLevel(int id)
    {
        if (game.level != null)
        {
            Destroy(game.level.gameObject);
        }
        game.cars.Clear();

        var path = $"{_config.path}/{id}";
        var prefab = Resources.Load<LevelComponent>(path);
        game.level = Instantiate(prefab);
        game.ttEvents.Clear();
        game.level.tribines.ForEach(x => x.Value.ResetViewers());
        ChangeGameState(GameStateID.CarSelection);

        screen.Text.SetActive(true);
    }

    private int GetLevelID(int levelID)
    {
        var levelCount = Resources.LoadAll<LevelComponent>(_config.path).Length;
        if (levelID < levelCount)
        {
            return levelID;
        }
        else
        {
            return new Random(levelID).Next(0, levelCount - 1);
        }
    }
}

