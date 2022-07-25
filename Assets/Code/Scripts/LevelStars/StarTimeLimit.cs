using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StarTimeLimit : LevelStar {

    private float _timeLimit = 60f;
    private float _timePast = 0f;


    public StarTimeLimit(float timeLimit) {
        star = true;
        _timeLimit = timeLimit;
    }

    public override void Update() {
        _timePast += Time.deltaTime;
        // Update UI clock.
        if (_timePast > _timeLimit) star = false;
    }

}
