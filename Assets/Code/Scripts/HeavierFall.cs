using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavierFall : MonoBehaviour {

    private Rigidbody _rb;
    [SerializeField] private float _multiplier = 1;
    private Vector3 _gravityAddForce = Vector3.down;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
        _gravityAddForce = Physics.gravity * _rb.mass * (_multiplier - 1);
    }

    void Update() {
        if (_rb.velocity.y < 0) _rb.AddForce(_gravityAddForce);
    }

}
