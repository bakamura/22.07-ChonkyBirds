using UnityEngine;

public static class PlayerInputs {

    private static Vector3 _movement;
    public static Vector3 Movement { get { return _movement; } set { _movement = value; } }
    private static bool _jumpKey;
    public static bool JumpKey { get { return _jumpKey; } set { _jumpKey = value; } }
    private static bool _pickKeyDown;
    public static bool PickKeyDown { get { return _pickKeyDown; } set { _pickKeyDown = value; } }
    private static bool _rollKeyDown;
    public static bool RollKeyDown { get { return _rollKeyDown; } set { _rollKeyDown = value; } }

}

//public interface PlayerInputs {

//    float GetHorizontalAxis();
//    float GetVerticalAxis();
//    bool IsJumping();
//    bool PickButtonDown();
//    bool RollButtonDown();

//}