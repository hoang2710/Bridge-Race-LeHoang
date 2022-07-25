using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstValues
{
    public const string PLAYER_ANIM_VELOCITY = "velocity";
    public const string PLAYER_ANIM_FALL = "fall";
    public const string PLAYER_ANIM_WIN = "win result";
    public const string PLAYER_ANIM_LOSE = "lose result";
    public const string TAG_PLAYER = "Player";
    public const string TAG_BRICK_SPAWN_POINT = "Brick Spawn Point";
    public const string TAG_SPAWN_LOCATION = "Spawn Location";
    public const float VALUE_STAIR_HEIGHT = 0.15f;
    public const float VALUE_STAIR_WIDTH = 0.18f;
    public const int VALUE_NUM_OF_PLAYER = 4;
    public const float VALUE_TIME_OF_FALL_ANIM = 4.85f;
    public const int LAYER_MASK_GROUND = 1 << 18;
    public const float VALUE_BOT_DETECT_RANGE = 6.0f;
    public const float VALUE_BOT_MIN_TOUCH_BRICK_DISTANCE = 0.06f * 0.06f; //NOTE: compare to sqrMag
    public const RigidbodyConstraints RB_PLAYER_MOVE_CONSTRAINTS = RigidbodyConstraints.FreezeRotation;
    public const RigidbodyConstraints RB_PLAYER_STAY_CONSTRAINTS = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    public const RigidbodyConstraints RB_BOT_DEFAULT_CONSTRAINTS = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
    public const float VALUE_TIME_FOR_BOT_GO_INTO_NEW_STAGE = 1.5f;
}
