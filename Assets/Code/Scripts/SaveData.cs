using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData {

    public bool[,,] levelStars = new bool[5, 15, 3]; // World, Level, Stars;
    public bool[] skins = new bool[15]; // Always Add to the end not the middle. If complicated, change for name bools
    public uint seed = 0;
    // Stamina system? 

}
