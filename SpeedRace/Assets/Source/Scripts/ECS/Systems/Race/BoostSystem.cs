using Kuhpik;
using System.Linq;
using UnityEngine;

public class BoostSystem : GameSystem
{
    private BoostSystemConfig _config;

    public override void OnStateEnter()
    {
        game.ttEvents.Commented += OnCommend;
        game.ttEvents.Gifted += OnGift;
    }

    public override void OnStateExit()
    {
        game.ttEvents.Commented -= OnCommend;
        game.ttEvents.Gifted -= OnGift;
    }

    public override void OnUpdate()
    {
        UpdateEffects();
        game.ttEvents.TryRaiseEvents();
    }

    private void OnGift(GiftData gift)
    {
        var giftInfo = config.cars.FirstOrDefault(x => x.Value.gift.id == gift.data.gift.gift_id);
        if (giftInfo.Value != null)
        {
            OnGift(giftInfo.Key);
            AddViewer(GetCarByIndex(giftInfo.Key).definition, gift.data.nickname);
        }
    }

    private void OnGift(int index)
    {
        var car = GetCarByIndex(index);
        ApplyBoost(car, _config.giftBoostSpeed, _config.giftBoostDuration, true);
    }

    private void OnCommend(CommentData comment)
    {
        if (int.TryParse(comment.data.comment, out var carNumber) && carNumber > 0 && carNumber <= game.cars.Count)
        {
            OnCommend(carNumber - 1);
            AddViewer(GetCarByIndex(carNumber - 1).definition, comment.data.nickname);
        }
    }

    private void OnCommend(int index)
    {
        var car = GetCarByIndex(index);
        ApplyBoost(car, _config.commentBoostSpeed, _config.commentBoostDuration, false);
    }

    private CarComponent GetCarByIndex(int index)
    {
        index = Mathf.Abs(index - (game.cars.Count - 1));
        return game.cars.FirstOrDefault(x => x.index == index);
    }

    public void ApplyBoost(CarComponent car, float speedBoost, float duration, bool gift)
    {
        car.isStarted = true;
        car.effects.Add(new BoostEffect(speedBoost, duration, gift));
        car.ShowBoost();
    }

    private void UpdateEffects()
    {
        foreach (var car in game.cars)
        {
            car.SetGiftBoost(car.effects.Any(x => x.gift));
            foreach (var effect in car.effects)
            {
                effect.Update(Time.deltaTime);
            }
            car.effects.RemoveAll(x => x.expired);
        }
    }

    private void AddViewer(CarDefinition carID, string name)
    {
        if (string.IsNullOrEmpty(name)) return;
        game.level.tribines[carID].AddViewer(name);

        Debug.Log(carID);
    }
}

