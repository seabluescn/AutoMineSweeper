using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineSweeper
{
    public enum GameState
    {
        Ready,
        Success,
        Fail
    }

    public enum MineState
    {
        HAVE = 1,
        HAVENOT = 0
    }

    public enum BoxState
    {
        Unknow = -1,
        None = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Mine = 9,
        MineBoom = 10,
        Flag = 11
    }

    public enum Grade
    {
        Easy = 20,
        Normal = 30,
        Hard = 40,
        Hell = 50
    }
}
