﻿namespace Kuhpik
{
    public enum EGamestate
    {
        // Don't change int values in the middle of development.
        // Otherwise all of your settings in inspector can be messed up.

        Loading = 1,
        Start = 2,
        Game = 3,
        Lose = 4,
        Win = 5,
        WinScreen = 6,
        Result = 10

        // Extend just like that
        //
        // Shop = 20,
        // Settings = 30,
        // Revive = 100
    }
}