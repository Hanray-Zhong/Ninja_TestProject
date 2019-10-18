using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Header("Game Input")]
    public GameInput Game_Input;
    [Header("Move")]
    public float MoveSpeed;
    public float JumpForce;
    public bool allowJump;
    public bool onHook;
    public float HookCircleRadius;
    [Header("Throw Cube")]
    public GameObject CubePrefab;
    public Transform ThrowPos;
    public float ThrowForce;
    [Range(0, 150)]
    public float CD_Time = 0;
    [Range(0, 150)]
    public float Bullet_Time = 0;
    

    // move
    private Vector2 moveDir;
    private bool faceRight;
    // jump
    private bool onGround;
    // private float jumpInteract;
    private float distance_toGround = 0.7f;
    // Throw Cube
    private bool allowThrow = true;
    private GameObject currentCube;
    private float lastGetThrowInteraction = 0;
    // Hook
    private float nearestDistance;
    private GameObject nearestHook = null;
    // Component
    private Rigidbody2D _rigidbody;
    private Transform _transform;

    private void Awake() {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _transform = gameObject.GetComponent<Transform>();
    }
    private void Update() {
        this.GetMoveDir();
        this.Move();
        this.Jump();
        this.ThrowCube();
        this.Flash();
        this.GetHook();
    }
    private void FixedUpdate() {
        this.CubeTimer();
    }

    private void GetMoveDir() {
        moveDir = Game_Input.GetMoveDir();
        if (moveDir.x > 0)
            faceRight = true;
        if (moveDir.x < 0)
            faceRight = false;
        // Debug.Log(moveDir);
    }
    private void Move() {
        // _rigidbody.AddForce(moveDir * MoveSpeed * Time.deltaTime, ForceMode2D.Force);
        // _transform.Translate(moveDir * MoveSpeed * Time.deltaTime, Space.World);
        if (!onHook) {
            _rigidbody.velocity = new Vector2(moveDir.x * MoveSpeed, _rigidbody.velocity.y);
            transform.rotation = Quaternion.identity;
        }

        if (onHook) {
            transform.up = nearestHook.transform.position - transform.position;
        }
    }
    private void Jump() {
        // jumpInteract = Game_Input.GetJumpInteraction();
        // if (jumpInteract > 0) {
        //     _rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        // }
        OnGround();
        if (onGround) 
            allowJump = true;
        if (Input.GetKeyDown(KeyCode.J) && allowJump) {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            allowJump = false;
        }
    }
    private void OnGround() {
        onGround = Physics2D.Raycast(transform.position, Vector2.down, distance_toGround, 1 << LayerMask.NameToLayer("Ground"));
    }
    private void ThrowCube() {
        if (Game_Input.GetThrowInteraction() == 1 && allowThrow) {
            Time.timeScale = 0.1f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            Bullet_Time++;
            Debug.Log(Game_Input.GetThrowInteraction());
        }
        if ((Game_Input.GetThrowInteraction() - lastGetThrowInteraction < 0 || Bullet_Time >= 150) && allowThrow) {
            currentCube = Instantiate(CubePrefab, ThrowPos.position, Quaternion.identity);
            currentCube.GetComponent<Cube>().Player = gameObject;
            Rigidbody2D cube_rigidbody = currentCube.GetComponent<Rigidbody2D>();
            if (cube_rigidbody != null) {
                 if (moveDir != Vector2.zero)
                    cube_rigidbody.AddForce(moveDir * ThrowForce, ForceMode2D.Impulse);
                else if (faceRight)
                    cube_rigidbody.AddForce(Vector2.right * ThrowForce, ForceMode2D.Impulse);
                else
                    cube_rigidbody.AddForce(Vector2.left * ThrowForce, ForceMode2D.Impulse);
            }
            CD_Time = 0;
            allowThrow = false;
            Bullet_Time = 0;
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        lastGetThrowInteraction = Game_Input.GetThrowInteraction();
    }
    private void CubeTimer() {
        if (CD_Time < 150)
            CD_Time++;
        if (CD_Time == 150)
            allowThrow = true;
        else
            allowThrow = false;
    }
    private void Flash() {
        if (!allowThrow && currentCube != null && CD_Time > 10) {
            if (!currentCube.GetComponent<Cube>().HitEnemy && Input.GetKeyDown(KeyCode.K)) {
                gameObject.transform.position = currentCube.transform.position;
                Destroy(currentCube);
                allowJump = true;
                Debug.Log("Flash!");
            }
        }
    }
    private void GetHook() {
        nearestDistance = HookCircleRadius;
        if (!onGround && !onHook) {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, HookCircleRadius, 1 << LayerMask.NameToLayer("Hook"));
            if (cols.Length != 0) {
                foreach (var col in cols) {
                    float currentDistance = Vector2.Distance(col.gameObject.transform.position, transform.position);
                    if (nearestDistance > currentDistance) {
                        nearestHook = col.gameObject;
                        nearestDistance = currentDistance;
                    }
                }
                if (Input.GetKeyDown(KeyCode.I)) {
                    onHook = true;
                    nearestHook.GetComponent<HingeJoint2D>().connectedBody = _rigidbody;
                }
            }
        }
        if (onHook) {
            
            if (Input.GetKeyUp(KeyCode.I)) {
                onHook = false;
                nearestHook.GetComponent<HingeJoint2D>().connectedBody = null;
            }
        }
    }
    

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -distance_toGround, 0));
        Gizmos.DrawWireSphere(transform.position, HookCircleRadius);
    }
}