using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPacifist : LevelStar {

    public override void Awake() {
        star = true;
        // Acess enemy script, tell them this is a goal, that script will change "star" in this in case of fail.
    }

}
