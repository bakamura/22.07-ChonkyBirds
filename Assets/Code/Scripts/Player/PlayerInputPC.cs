using UnityEngine;

public class PlayerInputPC : MonoBehaviour {

    [SerializeField] private float _inputRememberDuration = 0.2f;

    [Header("Input Keys")]

    [SerializeField] private KeyCode _forwardKey = KeyCode.W;
    public KeyCode ForwardKey { set { _forwardKey = value; } } // Make write-only ?
    [SerializeField] private KeyCode _backwardKey = KeyCode.S;
    public KeyCode BackwardKey { set { _backwardKey = value; } }
    [SerializeField] private KeyCode _leftKey = KeyCode.A;
    public KeyCode LeftKey { set { _leftKey = value; } }
    [SerializeField] private KeyCode _rightKey = KeyCode.D;
    public KeyCode RightKey { set { _rightKey = value; } }
    [SerializeField] private KeyCode _jumpKey = KeyCode.Mouse0;
    public KeyCode JumpKey { set { _jumpKey = value; } }

    [Space(16)]

    [SerializeField] private KeyCode _pickKey = KeyCode.Mouse0;
    public KeyCode PickKey { set { _pickKey = value; } }
    [SerializeField] private KeyCode _rollKey = KeyCode.Mouse1;
    public KeyCode RollKey { set { _rollKey = value; } }

    [Space(16)]

    [SerializeField] private KeyCode _pauseKey = KeyCode.Escape;
    public KeyCode PauseKey { set { _pauseKey = value; } }

    private void Update() {
        PlayerInputs.JumpKey -= Time.deltaTime;
        PlayerInputs.JumpKeyDown -= Time.deltaTime;
        PlayerInputs.PickKeyDown -= Time.deltaTime;
        PlayerInputs.RollKeyDown -= Time.deltaTime;

        PlayerInputs.Movement = new Vector3((Input.GetKey(_leftKey) ? -1 : 0) + (Input.GetKey(_rightKey) ? 1 : 0), 0, (Input.GetKey(_backwardKey) ? -1 : 0) + (Input.GetKey(_forwardKey) ? 1 : 0)).normalized;
        if (Input.GetKey(_jumpKey)) PlayerInputs.JumpKey = _inputRememberDuration;
        if (Input.GetKeyDown(_jumpKey)) PlayerInputs.JumpKeyDown = _inputRememberDuration;
        if (Input.GetKeyDown(_pickKey)) PlayerInputs.PickKeyDown = _inputRememberDuration;
        if (Input.GetKeyDown(_rollKey)) PlayerInputs.RollKeyDown = _inputRememberDuration;
    }

}
