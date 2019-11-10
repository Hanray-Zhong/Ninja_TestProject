using UnityEngine;

public class XBox_Input : GameInput {
    private Vector2 moveDir;
    private float hl;
	private float vt;
    private float jump_interact;
    private float throw_interact;
    private float hook_interact;

    public override Vector2 GetMoveDir() {
        hl = Input.GetAxis("XBox_Horizontal");
		vt = Input.GetAxis("XBox_Vertical");
        moveDir = new Vector2(hl, vt);
        moveDir = moveDir.normalized;
        if (moveDir.x >= -0.3827f && moveDir.x < 0.3827f) {
            if (moveDir.y > 0) moveDir = new Vector2(0, 1);
            else if (moveDir.y < 0) moveDir = new Vector2(0, -1);
            else moveDir = Vector2.zero;
        }
        else if (moveDir.x >= -0.9238f && moveDir.x < -0.3827f) {
            if (moveDir.y > 0) moveDir = new Vector2(-0.7071f, 0.7071f);
            else if (moveDir.y < 0) moveDir = new Vector2(-0.7071f, -0.7071f);
            else moveDir = Vector2.zero;
        }
        else if (moveDir.x >= 0.3827f && moveDir.x < 0.9238f) {
            if (moveDir.y > 0) moveDir = new Vector2(0.7071f, 0.7071f);
            else if (moveDir.y < 0) moveDir = new Vector2(0.7071f, -0.7071f);
            else moveDir = Vector2.zero;
        }
        else if (moveDir.x >= -1 && moveDir.x < -0.9238f) {
            moveDir = new Vector2(-1, 0);
        }
        else if (moveDir.x >= 0.9238f && moveDir.x <= 1) {
            moveDir = new Vector2(1, 0);
        }
        else moveDir = Vector2.zero;
        return moveDir;
    }
    public override float GetJumpInteraction() {
        jump_interact = Input.GetAxis("Jump");
        // if (jump_interact > 0) {
        //     Debug.Log("Get Jump.");
        // }
        return jump_interact;
    }
    public override float GetThrowInteraction() {
        throw_interact = Input.GetAxis("Throw");
        return throw_interact;
    }
    public override float GetHookInteraction() {
        hook_interact = Input.GetAxis("Hook");
        return hook_interact;
    }
}