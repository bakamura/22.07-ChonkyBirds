using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAngularVelocityToBall : MonoBehaviour {

    private Rigidbody _rb;
    [Tooltip("1 is an entire 360°")]
    [SerializeField] private Vector3 _angVelocity;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = _angVelocity;

        _rb.maxAngularVelocity = 50f;
        _angVelocity = _angVelocity * 2 * Mathf.PI;
        //_rb.angularVelocity = _angVelocity;

    }

    private void FixedUpdate() {
    }

}
