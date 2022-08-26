using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressData {

    // Game Mechanics

    private bool[,,] _levelStars = new bool[5, 15, 3]; // World, Level, Stars;
    public bool[,,] LevelStars { get { return _levelStars; } set { _levelStars = value; } }
    public bool[] _skins = new bool[15]; // Always Add to the end not the middle. If complicated, change for name bools
    private bool[] Skins { get { return _skins; } set { _skins = value; } }
    private uint _seed = 0;
    public uint Seed { get { return _seed; } set { _seed = value; } }
    // Stamina system? 

}
