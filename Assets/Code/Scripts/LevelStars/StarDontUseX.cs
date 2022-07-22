using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarDontUseX : LevelStar {

    private X _type = X.FallHard;
    public enum X {

        Jump,
        Flap,
        Pick,
        Eat,
        Roll,
        FallHard

    }

    public StarDontUseX(X type) {
        _type = type;
    }

    public override void Update() {
        
    }

}
