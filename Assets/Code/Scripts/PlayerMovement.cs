using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [Header("Components")]

    private Rigidbody _rb;

    [Header("Systems")]

    [SerializeField] private float _levelIntroDuration = 3;
    private bool _canMove = false;

    [Header("Walk")]

    [SerializeField] private float _groundMoveSpeed = 3;
    private float _movementSpeed = 3;
    [Tooltip("Time in seconds it takes for the Player to reach max speed")]
    [SerializeField] private float _accelerationDuration = 0.5f;
    private float _currentAcceleration = 0;
    [SerializeField] private float _turnDuration = 0.25f;
    private float _currentTurnVelocity = 0;

    [Header("Jump")]

    [Tooltip("Velocity added in Y when jumping")]
    [SerializeField] private float _jumpStrenght = 5;
    [SerializeField] private float _airMoveSpeed = 0.1f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Vector3 _groundCheckArea = Vector3.one;
    [SerializeField] private Vector3 _groundCheckOffset = Vector3.zero;
    private bool _isGrounded = false;
    [SerializeField] private float _wingFlapYVelocity = 0.25f;
    [SerializeField] private float _wingFlapMaxDuration = 3;
    private float _currentWingFlapTime = 0;
    [SerializeField] private float _delayToWingFlap = 0.6f;
    private Coroutine _currentWingFlapCoroutine = null;

    [Header("References")]

    private Transform _camera;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start() {
        _movementSpeed = _groundMoveSpeed;
        _accelerationDuration = _accelerationDuration * Time.fixedDeltaTime;
        _groundCheckArea /= 2;
        StartCoroutine(EndLevelIntro());

        _camera = Camera.main.transform;
    }

    private void FixedUpdate() {
        if (_canMove) {
            // Movement
            _currentAcceleration = Mathf.Clamp01(_currentAcceleration + (PlayerInputs.Movement.sqrMagnitude > 0 ? 1 : -1) / _accelerationDuration);

            float targetAngle = Mathf.Atan2(PlayerInputs.Movement.x, PlayerInputs.Movement.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            Vector3 velocity = (PlayerInputs.Movement.sqrMagnitude > 0 ? (Quaternion.Euler(0, targetAngle, 0) * Vector3.forward).normalized : _rb.velocity.normalized) * _movementSpeed * _currentAcceleration; // CHECK FOR BETTER FORMATTING
            if (PlayerInputs.Movement != Vector3.zero) transform.rotation = Quaternion.Euler(0, Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentTurnVelocity, _turnDuration), 0);

            // Jump
            bool wasGrounded = _isGrounded;
            _isGrounded = Physics.OverlapBox(transform.position + _groundCheckOffset, _groundCheckArea, Quaternion.identity, _groundLayer).Length > 0; // (MUST CHECK!)
            if (_isGrounded) {
                if (PlayerInputs.JumpKeyDown > 0) {
                    PlayerInputs.JumpKeyDown = 0;
                    _rb.velocity += Vector3.up * _jumpStrenght; // This takes in consideration Y velocity will always be 0 (MUST CHECK!)
                    velocity[1] = _rb.velocity.y;
                    _currentWingFlapCoroutine = StartCoroutine(DelayToWingFlap());
                }
                else {
                    _movementSpeed = _groundMoveSpeed;
                    velocity[1] = _rb.velocity.y;
                }
            }
            else {
                if (PlayerInputs.JumpKey > 0 && _currentWingFlapTime > 0) {
                    _movementSpeed = _groundMoveSpeed;
                    velocity[1] = _wingFlapYVelocity;
                    _currentWingFlapTime -= Time.deltaTime;

                }
                else {
                    velocity[1] = _rb.velocity.y;
                    _movementSpeed = _airMoveSpeed;
                }
            }
            // if (wasGrounded && wasGrounded != _isGrounded && _currentWingFlapCoroutine != null) _currentWingFlapTime = _wingFlapMaxDuration; REQUIRES DEBUGGING

            // Apply
            _rb.velocity = velocity;
        }
    }

    private IEnumerator EndLevelIntro() { // Probably should be moved to the camera
        yield return new WaitForSeconds(_levelIntroDuration);

        _canMove = true;
    }

    private IEnumerator DelayToWingFlap() { // Study possibility of passing references to IEnumerator
        if (_currentWingFlapCoroutine != null) StopCoroutine(_currentWingFlapCoroutine); // Prevent Coroutine Stacking
        _movementSpeed = _airMoveSpeed;
        _currentWingFlapTime = 0;

        yield return new WaitForSeconds(_delayToWingFlap);

        _currentWingFlapTime = _wingFlapMaxDuration;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        Gizmos.color = _isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireCube(transform.position + _groundCheckOffset, Application.isPlaying ? _groundCheckArea * 2 : _groundCheckArea);
    }
#endif

}
