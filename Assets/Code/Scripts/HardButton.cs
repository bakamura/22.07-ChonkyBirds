using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardButton : MonoBehaviour {

    [SerializeField] private float _necessaryVelocity = 7f;

    [Header("MoveObject")]

    [SerializeField] private GameObject _objToMove = null;
    [SerializeField] private Vector3 _objNewPos = Vector3.zero;
    [SerializeField] private float _duration = 1.5f;

    private void Awake() {
        _necessaryVelocity *= _necessaryVelocity;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.relativeVelocity.sqrMagnitude > _necessaryVelocity) StartCoroutine(Activate());
    }

    private IEnumerator Activate() {
        Vector3 startPos = _objToMove.transform.position;
        float currentStep = 0;
        while (currentStep < 1) {
            currentStep += Time.deltaTime / _duration;
            if (currentStep >= 1) currentStep = 1;
            _objToMove.transform.position = Vector3.Lerp(startPos, _objNewPos, currentStep);

            yield return null;
        }
    }

}
