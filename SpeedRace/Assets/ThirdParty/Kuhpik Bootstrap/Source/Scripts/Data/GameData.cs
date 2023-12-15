using System;
using System.Collections.Generic;

namespace Kuhpik
{
    [Serializable]
    public class GameData
    {
        public TTSDKEvents ttEvents = new TTSDKEvents();
        public LevelComponent level;
        public MovementZone MovementZone;

        public List<CarComponent> cars = new List<CarComponent>();
    }
}