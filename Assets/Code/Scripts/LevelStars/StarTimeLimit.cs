using UnityEngine.UI;
using TMPro;

public class StarTimeLimit : LevelStar{

    private float _timeLimit = 60f;
    private float _timePast = 0f;


    public override void Awake() {
        star = true;
    }

    public override void Update() {
        // Update UI clock.
        if (_timePast > _timeLimit) star = false;
    }

}
