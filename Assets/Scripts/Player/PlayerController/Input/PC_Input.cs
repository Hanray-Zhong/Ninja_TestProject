using UnityEngine;

public class PC_Input : GameInput {
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
        hl = Input.GetAxis("Horizontal");
		vt = Input.GetAxis("Vertical");
        inputDir = new Vector2(hl, vt);
        if (playerController.onBulletTime && inputDir == Vector2.zero) 
            return moveDir;
        inputDir = inputDir.normalized;
        moveDir = inputDir;
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