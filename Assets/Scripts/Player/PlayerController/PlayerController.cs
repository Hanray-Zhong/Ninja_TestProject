using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Header("Game Input")]
    public GameInput Game_Input;
    [Header("Move")]
    public float MoveSpeed;
    // move
    private Vector2 moveDir;
    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private void Awake() {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _transform = transform;
    }
    private void Update() {
        this.GetMoveDir();
        this.Move();
    }

    private void GetMoveDir() {
        moveDir = Game_Input.GetMoveDir();
    }
    private void Move() {
        // _rigidbody.AddForce(moveDir * MoveSpeed * Time.deltaTime, ForceMode2D.Force);
        // _transform.Translate(moveDir * MoveSpeed * Time.deltaTime, Space.World);
        _rigidbody.velocity = new Vector2(moveDir.x * MoveSpeed, _rigidbody.velocity.y);
    }
}