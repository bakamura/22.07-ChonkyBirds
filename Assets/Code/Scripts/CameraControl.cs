using UnityEngine;

public class CameraControl : MonoBehaviour {

    [SerializeField] private Vector3 _followOffSet = Vector3.up;

    [Header("Proxy")]

    private Transform _playerTransf = null;

    private void Start() {
        _playerTransf = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update() {
        transform.position = _playerTransf.position + _followOffSet; // PROVISORY
    }

}
