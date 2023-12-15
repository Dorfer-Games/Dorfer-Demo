using UnityEngine;
using UnityEngine.UI;

namespace Kuhpik
{
    /// <summary>
    /// Used to store game data. Change it the way you want.
    /// </summary>
    public class GameData
    {
        // Example (I use public fields for data, but u free to use properties\methods etc)
        // public float LevelProgress;
        // public Enemy[] Enemies;

        public bool masking;
        public bool seeBackward;
        public bool fear;
        
        public GameObject light;

        public GameObject victim;
        public GameObject victimGhost;
        public GameObject murder;
        public Scrollbar fearBar;
        public GameObject finish;
        public GameObject level;
    }
}