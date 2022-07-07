using UnityEngine;

public static class PlayerInputs {

    private static Vector3 _movement;
    public static Vector3 Movement { get { return _movement; } set { _movement = value; } }
    private static float _jumpKey;
    public static float JumpKey { get { return _jumpKey; } set { _jumpKey = value; } }
    private static float _jumpKeyDown;
    public static float JumpKeyDown { get { return _jumpKeyDown; } set { _jumpKeyDown = value; } }
    private static float _pickKeyDown;
    public static float PickKeyDown { get { return _pickKeyDown; } set { _pickKeyDown = value; } }
    private static float _rollKeyDown;
    public static float RollKeyDown { get { return _rollKeyDown; } set { _rollKeyDown = value; } }

}

//public interface PlayerInputs {

//    float GetHorizontalAxis();
//    float GetVerticalAxis();
//    float IsJumping();
//    float PickButtonDown();
//    float RollButtonDown();

//}