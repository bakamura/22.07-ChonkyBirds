using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityVelocityInverter : MonoBehaviour {

    private Collider _playerCol = null;
    private Rigidbody _playerRb = null;

    private void Awake() {
        _playerCol = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>();
        _playerRb = _playerCol.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other == _playerCol) _playerRb.velocity = new Vector3(_playerRb.velocity.x, -_playerRb.velocity.y, _playerRb.velocity.z);
    }

}
