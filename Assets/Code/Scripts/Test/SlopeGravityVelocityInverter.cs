using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeGravityVelocityInverter : MonoBehaviour {

    private SphereCollider _playerCol = null;
    private Rigidbody _playerRb = null;

    private void Awake() {
        _playerCol = GameObject.FindGameObjectWithTag("Player").GetComponent<SphereCollider>();
        _playerRb = _playerCol.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other) {
        if (_playerCol == other) { _playerRb.velocity = new Vector3(_playerRb.velocity.x, -_playerRb.velocity.y, _playerRb.velocity.z); print("a"); }
    }

}
