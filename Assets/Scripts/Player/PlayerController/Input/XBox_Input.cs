using UnityEngine;

public class XBox_Input : GameInput {
    private Vector2 inputDir;
    private Vector2 moveDir;
    private float hl;
	private float vt;
    private float jump_interact;
    private float throw_interact;
    private float hook_interact;
    private PlayerController playerController;
    private void Start() {
        playerController = gameObject.GetComponent<PlayerController>();
    }

    public override Vector2 GetMoveDir() {
        hl = Input.GetAxis("XBox_Horizontal");
		vt = Input.GetAxis("XBox_Vertical");
        inputDir = new Vector2(hl, vt);
        if (playerController.onBulletTime && inputDir == Vector2.zero)
            return moveDir;
        moveDir = inputDir;
        return moveDir;
    }
    public override Vector2 GetArrowDir() {
        hl = Input.GetAxisRaw("XBox_Horizontal");
        vt = Input.GetAxisRaw("XBox_Vertical");
        inputDir = new Vector2(hl, vt);
        inputDir = inputDir.normalized;
        if (inputDir.x >= -0.3827f && inputDir.x < 0.3827f) {
            if (inputDir.y > 0) inputDir = new Vector2(0, 1);
            else if (inputDir.y < 0) inputDir = new Vector2(0, -1);
            else inputDir = Vector2.zero;
        }
        else if (inputDir.x >= -0.9238f && inputDir.x < -0.3827f) {
            if (inputDir.y > 0) inputDir = new Vector2(-0.7071f, 0.7071f);
            else if (inputDir.y < 0) inputDir = new Vector2(-0.7071f, -0.7071f);
            else inputDir = Vector2.zero;
        }
        else if (inputDir.x >= 0.3827f && inputDir.x < 0.9238f) {
            if (inputDir.y > 0) inputDir = new Vector2(0.7071f, 0.7071f);
            else if (inputDir.y < 0) inputDir = new Vector2(0.7071f, -0.7071f);
            else inputDir = Vector2.zero;
        }
        else if (inputDir.x >= -1 && inputDir.x < -0.9238f) {
            inputDir = new Vector2(-1, 0);
        }
        else if (inputDir.x >= 0.9238f && inputDir.x <= 1) {
            inputDir = new Vector2(1, 0);
        }
        else inputDir = Vector2.zero;
        return inputDir;
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