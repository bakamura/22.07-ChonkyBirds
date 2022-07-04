using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [Header("Components")]

    private Rigidbody _rb;

    [Header("Walk")]

    [SerializeField] private float _walkSpeed = 3;
    [Tooltip("Time in seconds it takes for the Player to reach max speed")]
    [SerializeField] private float _accelerationDuration = 0.5f;
    private float _currentAcceleration = 0;
    [SerializeField] private float _turnSpeed = 0.25f;
    private float _currentTurnVelocity = 0;

    [Header("Jump")]

    [Tooltip("Velocity added in Y when jumping")]
    [SerializeField] private float _jumpStrenght;

    [Header("References")]

    private Camera _camera;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start() {
        _accelerationDuration = _accelerationDuration * Time.fixedDeltaTime;

        _camera = Camera.main;
    }

    private void FixedUpdate() {
        _currentAcceleration = Mathf.Clamp01(_currentAcceleration + (PlayerInputs.Movement.sqrMagnitude > 0 ? 1 : -1) / _accelerationDuration);
        print(_currentAcceleration);
        _rb.velocity = (PlayerInputs.Movement.sqrMagnitude > 0 ? PlayerInputs.Movement : _rb.velocity.normalized) * _walkSpeed * _currentAcceleration;
        transform.rotation = Quaternion.Euler(0, Mathf.SmoothDampAngle(transform.eulerAngles.y, Mathf.Atan2(PlayerInputs.Movement.x, PlayerInputs.Movement.z) * Mathf.Rad2Deg, ref _currentTurnVelocity, _turnSpeed), 0);
    }

}
