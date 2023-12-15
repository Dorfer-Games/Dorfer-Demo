using Kuhpik;
using System.Linq;
using UnityEngine;
using UnityTools.Extentions;

public class FakeEventSource : GameSystem
{
    [SerializeField] private bool _isActive;

    [SerializeField, Range(0, 1f)] private float _incorrectCommentChance;
    [SerializeField, Range(0, 1f)] private float _incorrectGiftChance;

    public void Update()
    {
        if (!_isActive) return;
        if (Input.GetKeyDown(KeyCode.C))
        {
            InvokeFakeComment();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            InvokeFakeGift();
        }
        InvokeBoost(KeyCode.Alpha1, 1);
        InvokeBoost(KeyCode.Alpha2, 2);
        InvokeBoost(KeyCode.Alpha3, 3);
        InvokeBoost(KeyCode.Alpha4, 4);

        InvokeBoostGift(KeyCode.Alpha1, 1);
        InvokeBoostGift(KeyCode.Alpha2, 2);
        InvokeBoostGift(KeyCode.Alpha3, 3);
        InvokeBoostGift(KeyCode.Alpha4, 4);
    }

    private void InvokeBoostGift(KeyCode input, int carNumber)
    {
        if (Input.GetKeyDown(input) && Input.GetKey(KeyCode.LeftShift))
        {
            var giftID = config.cars[carNumber - 1].gift.id;
            InvokeFakeGift(giftID);
        }
    }

    private void InvokeBoost(KeyCode input, int carNumber)
    {
        if (Input.GetKeyDown(input) && !Input.GetKey(KeyCode.LeftShift))
        {
            InvokeFakeComment(carNumber);
        }
    }

    private void InvokeFakeGift(int? giftID = null)
    {
        var gift = GenerateObject<GiftData>();
        if (Random.Range(0, 1f) < _incorrectGiftChance)
        {
            gift.data.gift.gift_id = config.cars.GetRandom().Value.gift.id;
        }
        else
        {
            gift.data.gift.gift_id = Random.Range(0, int.MaxValue);
        }
        if (giftID != null)
        {
            gift.data.gift.gift_id = (int)giftID;
        }
        gift.data.nickname = gift.data.gift.gift_id.ToString();
        game.ttEvents.AddGiftEvent(gift);

    }

    private void InvokeFakeComment(int? value = null)
    {
        var comment = GenerateObject<CommentData>();
        if (Random.Range(0, 1f) < _incorrectCommentChance)
        {
            comment.data.comment = Random.Range(1, config.carCount + 1).ToString();
        }
        else
        {
            comment.data.comment = "NOTHING";
        }
        if (value != null)
        {
            comment.data.comment = value.ToString();
        }
        comment.data.nickname = comment.data.comment;
        game.ttEvents.AddCommentEvent(comment);
    }

    private T GenerateObject<T>() where T : class, new()
    {
        return JsonUtility.FromJson<T>(JsonUtility.ToJson(new T()));
    }
}

