using UnityEngine;

public class StarCollectObject : LevelStar {

    private static bool duplicate = false;

    ~StarCollectObject() {
        duplicate = false; // May be unnessecary.
    }

    public override void Awake() {
        GameObject.FindGameObjectWithTag(duplicate? "StarGoalObj2" : "StarGoalObj").GetComponent<StarGoalObj>().StarCheckScript = this;
        duplicate = true;
    }

}
