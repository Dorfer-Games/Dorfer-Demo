using System;
using UnityEngine;

namespace Kuhpik
{
    [Serializable]
    public class GiftInfo
    {
        [field: SerializeField] public int giftID { get; private set; }
        [field: SerializeField] public int carIndex { get; private set; }
        [field: SerializeField] public Sprite sprite { get; private set; }
    }
}