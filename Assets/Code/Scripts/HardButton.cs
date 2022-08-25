using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardButton : MonoBehaviour {

    private void OnCollisionEnter(Collision collision) {
        if(collision.relativeVelocity.magnitude > 10) {

        }
    }

}
