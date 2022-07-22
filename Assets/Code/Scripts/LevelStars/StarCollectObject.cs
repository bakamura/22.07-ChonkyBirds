using UnityEngine;

public class StarCollectObject : LevelStar {

    public override void Awake() {
        GameObject.FindGameObjectWithTag("StarGoalObj").GetComponent<StarGoalObj>().StarCheckScript = this; // Implement a way to handle multiple goal objects properly
    }

}
