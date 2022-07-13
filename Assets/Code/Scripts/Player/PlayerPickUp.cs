using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour {

    [Header("Info")]

    [SerializeField] private Vector3 _pickUpOffSet = Vector3.zero; // Could be substituted by _beakPoint.position
    [SerializeField] private float _pickUpRange = 1f;
    [SerializeField] private float _pickUpDuration = 0.6f;
    [SerializeField] private float _releaseDuration = 0.4f;
    [SerializeField] private Transform _beakPoint = null;

    [Header("EggSack Info")]

    private Transform _eggObj = null;
    public Transform EggObj { get { return _eggObj; } set { _eggObj = value; } }
    [SerializeField] private Transform _eggSackPoint = null;

    [Header("Internal")]

    private Coroutine _currentCoroutine = null;
    private Transform _currentObj = null;
    public Transform currentObj { get { return _currentObj; } }

    const string Object = "Object";
    const string Food = "Food";
    const string Egg = "Egg";
    private WaitForSeconds _waitPick;
    private WaitForSeconds _waitRelease;


    private void Awake() {
        _waitPick = new WaitForSeconds(_pickUpDuration);
        _waitRelease = new WaitForSeconds(_releaseDuration);
    }

    private void Update() {
        if (_currentCoroutine == null && PlayerInputs.PickKeyDown > 0f) {
            if (_currentObj == null) _currentCoroutine = StartCoroutine(PickupObj());
            else _currentCoroutine = StartCoroutine(ReleaseObj());
        }
    }

    private IEnumerator PickupObj() {
        // Set Anim

        yield return new WaitForSeconds(_pickUpDuration);

        Collider[] objs = Physics.OverlapSphere(transform.position + _pickUpOffSet, _pickUpRange); // Needs to rotate to player facing direction
        bool foreachBreaker = false;
        foreach (Collider obj in objs) {
            switch (obj.tag) {
                case Object:
                    _currentObj = obj.transform;
                    _currentObj.transform.SetParent(_beakPoint, false);
                    _currentObj.localPosition = Vector3.zero;
                    _currentObj.localEulerAngles = Vector3.zero;
                    _currentObj.GetComponent<Rigidbody>().useGravity = false;
                    _currentObj.GetComponent<Rigidbody>().detectCollisions = false;
                    foreachBreaker = true;
                    break;
                case Food:
                    _currentObj = obj.transform;
                    _currentObj.transform.SetParent(_beakPoint, false);
                    _currentObj.localPosition = Vector3.zero;
                    _currentObj.localEulerAngles = Vector3.zero;
                    _currentObj.GetComponent<Rigidbody>().useGravity = false;
                    _currentObj.GetComponent<Rigidbody>().detectCollisions = false;
                    foreachBreaker = true;
                    break;
                case Egg:
                    if (_eggObj == null) {
                        _eggObj = obj.transform;
                        _eggObj.transform.SetParent(_eggSackPoint, false);
                        _eggObj.localPosition = Vector3.zero;
                        _eggObj.localEulerAngles = Vector3.zero;
                        _eggObj.GetComponent<Rigidbody>().useGravity = false;
                        _eggObj.GetComponent<Rigidbody>().detectCollisions = false;
                        foreachBreaker = true;
                    }
                    else {
                        // Draw image to inform can't pick more than one at a time
                    }
                    break;
            }
            if (foreachBreaker) break;
        }
    }

    private IEnumerator ReleaseObj() {
        // Set Anim

        yield return new WaitForSeconds(_releaseDuration);
        
        _currentObj.SetParent(null, true);
        _currentObj.GetComponent<Rigidbody>().useGravity = true;
        _currentObj.GetComponent<Rigidbody>().detectCollisions = true;
        _currentObj = null;
    }

#if UNITY_EDITOR

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + _pickUpOffSet, _pickUpRange);
    }

#endif

}
