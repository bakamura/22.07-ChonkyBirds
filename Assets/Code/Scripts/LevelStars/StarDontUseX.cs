using System;
using UnityEngine;

public class StarDontUseX : LevelStar {

    private Action _updateAction = null;
    public enum X {

        Jump,
        Flap,
        Pick,
        Eat,
        Roll,
        FallHard

    }

    public StarDontUseX(X type) {
        _updateAction = GetUpdate(type);
    }

    public override void Update() {
        _updateAction.Invoke();
    }

    private void NJump() {

    }

    private void NFlap() {

    }

    private void NPick() {

    }

    private void NEat() {

    }

    private void NRoll() {

    }

    private void NFallHard() {

    }

    private Action GetUpdate(X type) {
        switch (type) {
            case X.Jump: return NJump;
            case X.Flap: return NFlap;
            case X.Pick: return NPick;
            case X.Eat: return NEat;
            case X.Roll: return NRoll;
            case X.FallHard: return NFallHard;
            default: Debug.Log("Error in StarDontUseX"); return null;
        }
    }

}
