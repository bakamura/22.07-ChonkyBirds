using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoll : MonoBehaviour {

    //Vars

    private IEnumerator Roll() {
        //change collider to sphere and check rigidbody constraints.
        // rotate player to input angle if not alraedy

        // add velocity (angular?)

        yield return null; // For Compile's sake
        // After a few seconds, check if velocity is lower than a treshold and if so, stop roll, else keep checking every few seconds
    }

}
