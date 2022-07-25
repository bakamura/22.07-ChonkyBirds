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

    private Action _mainGoalCheck = null;  // More memory allocation for a small increase in speed
    private LevelStar _secondGoalCheck = null;
    private LevelStar _thirdGoalCheck = null;

    [SerializeField] private float _timeLimit = 60f; //

    [Header("Proxy")]

    private Collider _playerCol = null;
    private PlayerMovement _playerMovementScript = null;
    private PlayerPickUp _playerPickScript = null;

    const string Food = "Food";
    const string Fed = "Fed";

    private void Awake() {
        if (_mainGoal == NestType.Eggs) _eggObjs = new Transform[_eggPos.Length];

        _mainGoalCheck = GetMainGoalCheck();
    }

    private void Start() {
        _playerCol = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>();
        _playerMovementScript = _playerCol.GetComponent<PlayerMovement>();
        _playerPickScript = _playerCol.GetComponent<PlayerPickUp>();

        _secondGoalCheck = GetSecondaryGoalCheck(_secondGoal);
        _thirdGoalCheck = GetSecondaryGoalCheck(_thirdGoal);
    }

    private void Update() {
        _secondGoalCheck?.Update();
        _thirdGoalCheck?.Update();
    }

    private void OnTriggerEnter(Collider other) {
        if (other == _playerCol) _mainGoalCheck.Invoke();
    }

    private void LevelClear() {
        // Calc Stars, show UI
        Debug.Log("Stars Got: *" + (_secondGoalCheck.star ? " *" : " _") + (_thirdGoalCheck.star ? " *" : " _"));
        _secondGoalCheck = null; // To prevent .Update being called
        _thirdGoalCheck = null;
        // Save Progress
        // Level Clear PopUp
    }

    private void ReturnNestCheck() {
        // Maybe make separate check for dirty
        if (_playerMovementScript.IsDirty) { } // Show popup that it's dirty?
        else LevelClear();
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
                    if (i == _hatchlings.Length - 1) LevelClear();
                    break;
                }
        }
    }

    private bool DontJumpGoal() {
        return false;

    }

    private bool DontPickGoal() {
        return false;

    }

    private bool DontEatGoal() {
        return false;

    }

    private bool DontRollGoal() {
        return false;

    }

    private bool DontHitHardGoal() {
        return false;

    }

    private Action GetMainGoalCheck() {
        switch (_mainGoal) {
            case NestType.Return: return ReturnNestCheck;
            case NestType.Eggs: return EggNestCheck;
            case NestType.Hatchlings: return HatchlingNestCheck;
            default: return null;
        }
    }

    private LevelStar GetSecondaryGoalCheck(SecondaryGoal goal) {
        switch (goal) {
            case SecondaryGoal.BringObject: return new StarCollectObject();
            case SecondaryGoal.TimeLimit: return new StarTimeLimit(_timeLimit);
            case SecondaryGoal.Pacifist: return new StarPacifist();
            case SecondaryGoal.DontJump: return new StarDontUseX(StarDontUseX.X.Jump);
            case SecondaryGoal.DontPick: return new StarDontUseX(StarDontUseX.X.Pick);
            case SecondaryGoal.DontEat: return new StarDontUseX(StarDontUseX.X.Eat);
            case SecondaryGoal.DontRoll: return new StarDontUseX(StarDontUseX.X.Roll);
            case SecondaryGoal.DontHitHard: return new StarDontUseX(StarDontUseX.X.FallHard);
            default: return null;
        }
    }

}
