using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Header("Game Input")]
    public GameInput Game_Input;
    [Header("Move")]
    public float MoveSpeed;
    public float JumpForce;
    // move
    private Vector2 moveDir;
    // jump
    // private float jumpInteract;
    private bool allowJump;
    private float distance_toGround = 0.65f;
    private Rigidbody2D _rigidbody;
    private Transform _transform;

    private void Awake() {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _transform = gameObject.GetComponent<Transform>();
    }
    private void Update() {
        this.GetMoveDir();
        this.Move();
        Jump();
    }

    private void GetMoveDir() {
        moveDir = Game_Input.GetMoveDir();
    }
    private void Move() {
        // _rigidbody.AddForce(moveDir * MoveSpeed * Time.deltaTime, ForceMode2D.Force);
        // _transform.Translate(moveDir * MoveSpeed * Time.deltaTime, Space.World);
        _rigidbody.velocity = new Vector2(moveDir.x * MoveSpeed, _rigidbody.velocity.y);
    }
    private void Jump() {
        // jumpInteract = Game_Input.GetJumpInteraction();
        // if (jumpInteract > 0) {
        //     _rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        // }
        OnGround();
        if (Input.GetKeyDown(KeyCode.J) && allowJump) {
            _rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
    }
    private void OnGround() {
        allowJump = Physics2D.Raycast(transform.position, Vector2.down, distance_toGround, 1 << LayerMask.NameToLayer("Ground"));
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -distance_toGround, 0));
    }
}