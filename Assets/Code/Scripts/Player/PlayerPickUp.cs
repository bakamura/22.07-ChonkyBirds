using System.Collections;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour {

    [Header("Info")]

    [SerializeField] private Vector3 _pickUpOffSet = Vector3.zero; // Could be substituted by _beakPoint.position
    [SerializeField] private float _pickUpRange = 1f;
    [SerializeField] private float _pickUpDuration = 0.6f;
    [SerializeField] private float _releaseDuration = 0.4f;
    [SerializeField] private Transform _beakPoint = null;

    [Header("Poop")]

    [SerializeField] private GameObject _poopPrefab = null;
    [SerializeField] private float _delayToPoop = 5f;
    [SerializeField] private Vector3 _poopOffSet = Vector3.down;
    // UI indicator clock for poop

    [Header("EggSack Info")]

    private Transform _eggObj = null;
    public Transform EggObj { get { return _eggObj; } set { _eggObj = value; } }
    [SerializeField] private Transform _eggSackPoint = null;

    [Header("Internal")]

    private Coroutine _currentCoroutine = null; // Move elsewhere?
    public Coroutine CurrentCoroutine { get { return _currentCoroutine; } set { _currentCoroutine = value; } }
    private Transform _currentObj = null;
    public Transform CurrentObj { get { return _currentObj; } }

    const string Object = "Object";
    const string Food = "Food";
    const string Egg = "Egg";
    private WaitForSeconds _waitPick = null;
    private WaitForSeconds _waitRelease = null;
    private WaitForSeconds _waitPoop = null;

    [Header("Proxy")]

    private PlayerMovement _movementScript;
    private Rigidbody _rb;

    private void Awake() {
        _movementScript = GetComponent<PlayerMovement>();
        _rb = GetComponent<Rigidbody>();

        _waitPick = new WaitForSeconds(_pickUpDuration);
        _waitRelease = new WaitForSeconds(_releaseDuration);
        _waitPoop = new WaitForSeconds(_delayToPoop / 5);
    }

    private void Update() {
        if (_currentCoroutine == null && _movementScript.IsGrounded && PlayerInputs.PickKeyDown > 0f) {
            if (_currentObj == null) _currentCoroutine = StartCoroutine(PickupObj());
            else _currentCoroutine = StartCoroutine(ReleaseObj());
        }
        if (PlayerInputs.RollKeyDown > 0 && _currentObj != null && _currentObj.CompareTag(Food)) StartCoroutine(InstantiatePoop()); // Check Conditions
    }

    private IEnumerator PickupObj() {
        _rb.velocity = Vector3.zero;
        _movementScript.CanMove = false;
        // Set Anim

        yield return _waitPick;

        Collider[] objs = Physics.OverlapSphere(transform.position + (Quaternion.Euler(0, transform.eulerAngles.y, 0) * _pickUpOffSet), _pickUpRange); // Needs to rotate to player facing direction
        bool foreachBreaker = false;
        foreach (Collider obj in objs) {
            switch (obj.tag) {
                case Object:
                    SetupObject(obj, ref _currentObj, ref _beakPoint);
                    foreachBreaker = true;
                    break;
                case Food:
                    SetupObject(obj, ref _currentObj, ref _beakPoint);
                    // Set roll btn to eat.
                    foreachBreaker = true;
                    break;
                case Egg:
                    if (_eggObj == null) {
                        SetupObject(obj, ref _eggObj, ref _eggSackPoint);
                        foreachBreaker = true;
                    }
                    else {
                        // Draw image to inform can't pick more than one at a time
                    }
                    break;
            }
            if (foreachBreaker) break;
        }
        _movementScript.CanMove = true;

        _currentCoroutine = null;
    }

    private void SetupObject(Collider item, ref Transform itemSlot, ref Transform newParent) {
        itemSlot = item.transform;
        itemSlot.SetParent(newParent, false);
        itemSlot.localPosition = Vector3.zero;
        itemSlot.localEulerAngles = Vector3.zero;
        Rigidbody itemRb = itemSlot.GetComponent<Rigidbody>();
        itemRb.velocity = Vector3.zero;
        itemRb.useGravity = false;
        itemRb.detectCollisions = false;
        itemRb.constraints = RigidbodyConstraints.FreezeAll; //
    }

    private IEnumerator ReleaseObj() {
        _rb.velocity = Vector3.zero;
        _movementScript.CanMove = false;
        // Set Anim

        yield return _waitRelease;

        _currentObj.SetParent(null, true);
        Rigidbody itemRb = _currentObj.GetComponent<Rigidbody>();
        itemRb.useGravity = true;
        itemRb.detectCollisions = true;
        itemRb.constraints = RigidbodyConstraints.None; //
        _currentObj = null;
        _movementScript.CanMove = true;

        _currentCoroutine = null;
    }

    private IEnumerator InstantiatePoop() {
        _currentObj = null;
        //Start timer clock
        for (int i = 0; i < 5; i++) { // SUBISTITUTE FOR A SMOOTH CLOCK IN UPDATE ?
            // Fill UI image based on 'i'
            
            yield return _waitPoop;
        }
        // Set UI image to full (?)

        Instantiate(_poopPrefab, transform.position + _poopOffSet, Quaternion.identity);

        // Delay to erase clock UI
        
        // Close clock UI
    }

#if UNITY_EDITOR

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + (Quaternion.Euler(0, transform.eulerAngles.y, 0) * _pickUpOffSet), _pickUpRange);
    }

#endif

}