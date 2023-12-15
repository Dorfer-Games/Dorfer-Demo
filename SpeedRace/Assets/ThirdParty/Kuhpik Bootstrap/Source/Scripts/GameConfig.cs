using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kuhpik
{
    [CreateAssetMenu(menuName = "Config/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public CarsDict cars { get; private set; }
        [field: SerializeField] public float baseCarSpeed { get; private set; }
        [field: SerializeField] public float targetPointsAmmount { get; private set; }
        [field: SerializeField] public float resultDuration { get; private set; }
        [field: SerializeField] public PointsDisplay loopPointsDisplay { get; private set; }

        public int carCount => cars.Count;
    }

    [Serializable]
    public class CarsDict : SerializableDictionary<int, CarDefinition> { }

}