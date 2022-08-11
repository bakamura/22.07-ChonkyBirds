using System.Collections;
using UnityEngine;

public class PlayerRoll : MonoBehaviour {

    [SerializeField] private float _rollSpeed = 5f;

    private Rigidbody _rb = null;
    private MeshCollider _meshCol = null;
    private SphereCollider _sphereCol = null;
    
    private PlayerMovement _movementScript = null;
    private PlayerPickUp _pickupScript = null;
    private Transform _camera = null;
    private WaitForSeconds _sleep = new WaitForSeconds(0.1f);

    private void Awake() {
        _rb = GetComponent<Rigidbody>();

        _meshCol = GetComponent<MeshCollider>();
        _sphereCol = GetComponent<SphereCollider>();
        _sphereCol.enabled = false;

        _movementScript = GetComponent<PlayerMovement>();
        _pickupScript = GetComponent<PlayerPickUp>();
        _camera = Camera.main.transform;
    }

    private void Update() {
        if (_pickupScript.CurrentCoroutine == null && _movementScript.IsGrounded && PlayerInputs.RollKeyDown > 0) StartCoroutine(Roll());
    }

    private IEnumerator Roll() {
        _movementScript.CanMove = false;
        _meshCol.enabled = false;
        _sphereCol.enabled = true;
        _rb.constraints = RigidbodyConstraints.None;

        if(PlayerInputs.Movement != Vector3.zero) transform.eulerAngles = new Vector3(0, Mathf.Atan2(PlayerInputs.Movement.x, PlayerInputs.Movement.z) * Mathf.Rad2Deg + _camera.eulerAngles.y, 0);
        _rb.velocity = transform.forward * _rollSpeed;
        _rb.angularVelocity = Vector3.forward * _rollSpeed;

        yield return new WaitForSeconds(1.5f); //
        while (_rb.velocity.sqrMagnitude > 2) yield return _sleep;

        _meshCol.enabled = true;
        _sphereCol.enabled = false;
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _rb.velocity = Vector3.zero;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        _movementScript.CanMove = true;
    }

}
