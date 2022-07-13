using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour {

    [Header("Info")]

    [SerializeField] private NestType _nestType = NestType.Normal;
    private enum NestType {

        Normal,
        Eggs,
        Hatchlings,

    }

    [Header("Egg")]

    private Vector3[] _eggPos = new Vector3[1];
    private Transform[] _eggObjs = null;

    [Header("Proxy")]

    private GameObject _playerGO = null;

    private void Awake() {
        _eggObjs = new Transform[_eggPos.Length];
    }

    private void Start() {
        _playerGO = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == _playerGO) {
            switch (_nestType) {
                case NestType.Normal:
                    // Calc Stars
                    // Save Progress
                    // Level Clear PopUp
                    break;
                case NestType.Eggs:
                    _playerGO.GetComponent<PlayerPickUp>().EggObj.transform.SetParent(null);
                    _playerGO.GetComponent<PlayerPickUp>().EggObj.transform.position = EggPosition();
                    
                    break;
            }
        }
    }

    private Vector3 EggPosition() {
        for (int i = 0; i < _eggObjs.Length; i++) {
            if (_eggObjs[i] != null) return _eggPos[i];
        }
        Debug.Log("Error Trying to get egg position in nest");
        return Vector3.zero;
    }

}
