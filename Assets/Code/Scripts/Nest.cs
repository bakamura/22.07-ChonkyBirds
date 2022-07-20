using System;
using UnityEngine;

public class Nest : MonoBehaviour {

    [Header("Info")]

    [SerializeField] private NestType _mainGoal = NestType.Return;
    [SerializeField] private SecondaryGoal _secondGoal = SecondaryGoal.TimeLimit;
    [SerializeField] private SecondaryGoal _thirdGoal = SecondaryGoal.TimeLimit;
    private enum NestType {

        Return,
        Eggs,
        Hatchlings,

    }
    // Rethink. Could be done using statemachine-like code
    private enum SecondaryGoal { 

        BringObject,
        TimeLimit,
        Pacifist,
        DontJump,
        DontPick,
        DontEat,
        DontRoll,
        DontHitHard,

    }

    [Header("Egg")]

    [SerializeField] private Vector3[] _eggPos = new Vector3[1];
    private Transform[] _eggObjs = null;

    [Header("Hatchlings")]

    [SerializeField] private Animator[] _hatchlings = new Animator[1]; // change animation into satisfied animation
    // pop up object?

    [Header("Stars")]

    private bool[] _stars = new bool[2];
    private Action _mainGoalCheck = null;  // More memory allocation for a small increase in speed
    private Action _secondGoalCheck = null;
    private Action _thirdGoalCheck = null;

    [Header("Proxy")]

    private Collider _playerCol = null;
    private PlayerPickUp _playerPickScript = null;

    const string Egg = "Egg";
    const string Food = "Food";
    const string Fed = "Fed";

    private void Awake() {
        _eggObjs = new Transform[_eggPos.Length];

        _mainGoalCheck = GetMainGoalCheck();
        _secondGoalCheck = GetSecondaryGoalCheck(_secondGoal);
        _thirdGoalCheck = GetSecondaryGoalCheck(_thirdGoal);
    }

    private void Start() {
        _playerCol = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>();
        _playerPickScript = _playerCol.GetComponent<PlayerPickUp>();
    }

    private void Update() {
        _secondGoalCheck.Invoke();
        _thirdGoalCheck.Invoke();
    }

    private void OnTriggerEnter(Collider other) {
        if (other == _playerCol) _mainGoalCheck.Invoke();
    }

    private void LevelClear() {
        print("Level Cleared!");
        // Calc Stars
        // Save Progress
        // Level Clear PopUp
    }

    private void ReturnNestCheck() {
        // Check if is clean
        LevelClear();
    }

    private void EggNestCheck() {
        if (_playerPickScript.EggObj != null) {
            _playerPickScript.EggObj.transform.SetParent(null);
            _playerPickScript.EggObj.transform.position = EggPosition();
            if (_eggObjs[_eggObjs.Length - 1] != null) LevelClear();
        }
    }

    private Vector3 EggPosition() {
        for (int i = 0; i < _eggObjs.Length; i++) if (_eggObjs[i] == null) {
                _eggObjs[i] = _playerPickScript.EggObj;
                _playerPickScript.EggObj = null;
                return _eggPos[i];
            }
        Debug.Log("Error Trying to get egg position in nest");
        return Vector3.zero;
    }

    private void HatchlingNestCheck() {
        if (_playerPickScript.CurrentObj.CompareTag(Food)) { // Deal with null possobility somehow
            _playerPickScript.CurrentObj.GetComponent<MeshRenderer>().enabled = false;  //
            _playerPickScript.CurrentObj.transform.SetParent(null);                     // Simply destroy the object?
            for (int i = 0; i < _hatchlings.Length; i++) if (!_hatchlings[i].GetBool(Fed)) {
                    _hatchlings[i].SetBool(Fed, true); // TEST how it functions with animator attached
                    if(i == _hatchlings.Length - 1) LevelClear();
                    break;
                }
        }
    }

    private void BringObjectGoal() {

    }

    private void TimeLimitGoal() {

    }

    private void PacifistGoal() {

    }

    private void DontJumpGoal() {

    }

    private void DontPickGoal() {

    }

    private void DontEatGoal() {

    }

    private void DontRollGoal() {

    }

    private void DontHitHardGoal() {

    }

    private Action GetMainGoalCheck() {
        switch (_mainGoal) {
            case NestType.Return: return ReturnNestCheck;
            case NestType.Eggs: return EggNestCheck;
            case NestType.Hatchlings: return HatchlingNestCheck;
            default: return null;
        }
    }

    private Action GetSecondaryGoalCheck(SecondaryGoal goal) {
        switch (goal) {
            case SecondaryGoal.BringObject: return BringObjectGoal;
            case SecondaryGoal.TimeLimit: return TimeLimitGoal;
            case SecondaryGoal.Pacifist: return PacifistGoal;
            case SecondaryGoal.DontJump: return DontJumpGoal;
            case SecondaryGoal.DontPick: return DontPickGoal;
            case SecondaryGoal.DontEat: return DontEatGoal;
            case SecondaryGoal.DontRoll: return DontRollGoal;
            case SecondaryGoal.DontHitHard: return DontHitHardGoal;
            default: return null;
        }
    }

}
