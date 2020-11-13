using UnityEngine;

public class PC_Input : GameInput {
    private Vector2 inputDir;
    public Vector2 lastInputDir { get; set; }

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

        // ...

        moveDir = inputDir;
        return moveDir;
    }
    public override Vector2 GetArrowDir() {
        hl = Input.GetAxisRaw("Horizontal");
        vt = Input.GetAxisRaw("Vertical");
        inputDir = new Vector2(hl, vt);

        if (!playerController.onBulletTime)
        {
            lastInputDir = playerController.FaceRight ? new Vector2(1, 0) : new Vector2(-1, 0);
        }
        if (inputDir.magnitude < 1)
        {
            return lastInputDir;
        }

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
        lastInputDir = inputDir;
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