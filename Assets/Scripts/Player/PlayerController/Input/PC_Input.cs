using UnityEngine;

public class PC_Input : GameInput {
    private Vector2 moveDir;
    private float hl;
	private float vt;
    private float jump_interact;
    private float throw_interact;

    public override Vector2 GetMoveDir() {
        hl = Input.GetAxis("Horizontal");
		vt = 0;
        moveDir = new Vector2(hl, vt).normalized;
        return moveDir;
    }
    public override float GetJumpInteraction() {
        jump_interact = Input.GetAxis("Jump");
        if (jump_interact > 0) {
            Debug.Log("Get Jump.");
        }
        return jump_interact;
    }
    public override float GetThrowInteraction() {
        throw_interact = Input.GetAxis("Throw");
        if (throw_interact > 0) {
            Debug.Log("Get Throw.");
        }
        return throw_interact;
    }
}