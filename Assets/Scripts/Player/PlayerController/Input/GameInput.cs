using UnityEngine;

public abstract class GameInput : MonoBehaviour {
    public abstract Vector2 GetMoveDir();
    public abstract float GetJumpInteraction();
    public abstract float GetThrowInteraction();
}