using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // 单例模式
    private static PlayerController _instance;
    public static PlayerController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Player").GetComponent<PlayerController>();
            }
            return _instance;
        }
    }


    // 人物未死亡，只是不能控制
    public bool isControlled = true;

    #region 游戏输入
    [Header("Game Input")]
    public GameInput PlayerGameInput;
    public Dropdown dropdown;
    #endregion

    #region 移动：属性字段
    [Header("Move")]
    private Vector2 deltaMoveDir;
    private Vector2 lastMoveDir;
    public Vector2 MoveDir { get; set; }
    public bool FaceRight { get; set; } = true;
    public float MoveSpeed;
    public float NormalSpeed { get; set; }
    public float OutSideSpeed { get; set; }
    public AnimationCurve SpeedCurve;
    public float StartMoveTime;
    public float EndMoveTime;
    [Range(-1, 1)]
    public float moveTimer;

    public Vector2 playerVelocity = Vector2.zero;
    public float x_Max_Velocity;
    public float x_basicLimitDecreaseRate;
    public float x_limitDecreaseRate;
    public float x_decreaseRateWithoutControl;
    public float y_Max_Velocity;
    public float Max_Velocity;
    #endregion

    #region Jump Properties
    [Header("Jump")]
    public float JumpSpeed;
    public AnimationCurve GravityCurve;
    private float oriGravity;
    private float delta_JumpInteraction;
    private float lastJumpInteraction = 0;
    public bool AllowJump { get; set; } = false;
    public bool SecJump { get; set; } = false;

    private bool jumpInteraction = false;
    private float jumpInteraction_CDTimer;
    private bool startjumpInteraction_CDTimer = false;
    // 土狼时间
    private bool startCoyoteTimer = false;
    private float coyoteTimer = 0;
    // 跳跃缓冲
    private bool startJumpBufferTimer = false;
    private float jumpBufferTimer = 0;

    [Header("OnGround Collider")]
    public Vector2 OnGroundColliderCenter;
    public Vector2 OnGroundColliderSize;
    public bool OnGround { get; set; }
    private bool lastOnGround = true;
    private bool just_OnGround = false;
    #endregion

    #region Public Throw Cube
    [Header("Throw Cube")]
    public bool allowThrowCube;
    public GameObject CubePrefab;
    public Transform ThrowPos;
    public float ThrowForce;
    public GameObject Arrow;
    [Range(0, 150)]
    public float CubeCDTimer = 0;
    [Range(0, 150)]
    public float Bullet_Timer = 0;
    [HideInInspector]
    public bool onBulletTime = false;
    public GameObject FlashEffect;
    public TrailRenderer trailRenderer;
    #endregion

    [Header("Carry NPC")]
    #region Carry NPC
    public bool canCarryNPC;
    public GameObject carriedNPC;
    public float offset_NpcToPlayer;
    #endregion

    [Header("Throw Cube")]
    #region Throw Cube
    private bool canThrowCube = true;
    private Cube currentCube;
    private float lastThrowInteraction = 0;
    private float delta_ThrowInteraction;
    private SpriteRenderer sprite;
    #endregion

    [Header("Flash")]
    private bool FlashOver = true;
    public bool isInvincible { get; private set; }

    [Header("Link")]
    #region Link Properties
    // Ability Allowance
    public bool AllowLink = false;
    public bool allowLink { get { return AllowLink; } set { AllowLink = value; } }
    public bool onHook;
    public bool JustReleaseHook;
    public float HookCircleRadius;
    public float SwingForce;
    [Range(0, 99999)]
    public float Hang_Time = 0;
    [Range(0, 150)]
    public float Hook_CD_Time = 150;
    public float ClimbSpeed;
    public float Max_RopeLength;
    public float Min_RopeLength;
    public LineRenderer Rope;
    #endregion

    [Header("Hook")]
    #region Hook
    private float nearestDistance;
    public GameObject NearestHook { get; set; }
    private GameObject currentHook;
    private Vector2 velocity_beforeHook;
    private bool justHook = false;
    private float lastHookInteraction = 0;
    private float delta_HookInteraction;
    #endregion

    #region Component
    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private PlayerUnit _unit;
    #endregion

    private void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _transform = gameObject.GetComponent<Transform>();
        _unit = gameObject.GetComponent<PlayerUnit>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        NormalSpeed = MoveSpeed;
        oriGravity = _rigidbody.gravityScale;
    }
    private void Update()
    {
        if (isControlled && !_unit.IsDead)
            GetMoveDir();
    }
    private void LateUpdate()
    {
        IsOnGround();
        // 处于不允许玩家控制阶段
        if (!isControlled)
        {
            _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
            ResetPlayerControllerStatus();
            return;
        }
        // 处于允许玩家控制阶段
        if (_unit.IsDead == false)
        {
            Move();
            Jump();
            CheckCubeCD();
            if (allowThrowCube)
                ThrowCube();
            else
                CarryNPC();
            Flash();
            if (allowLink)
                GetHook();
            OnHook();
        }
    }
    private void FixedUpdate()
    {
        MoveTimer();
        JumpBufferTimer();
        CoyoteTimer();
        JumpInteraction_CDTimer();
        CubeTimer();
    }

    private void GetMoveDir()
    {
        MoveDir = PlayerGameInput.GetMoveDir();
        if (MoveDir.x > 0)
            FaceRight = true;
        if (MoveDir.x < 0)
            FaceRight = false;
        // Debug.Log(MoveDir);
    }
    private void Move()
    {
        playerVelocity = _rigidbody.velocity;
        // 正常移动
        if (!onHook && Bullet_Timer == 0)
        {
            _rigidbody.freezeRotation = true;
            _rigidbody.gravityScale = oriGravity;
            _transform.rotation = Quaternion.identity;
            // 预测速度
            playerVelocity = new Vector2(SpeedCurve.Evaluate(moveTimer) * MoveSpeed, _rigidbody.velocity.y);
            // 限制速度（仅限在空中）
            if (!OnGround)
            {
                // x 递减至最大速度
                if ((Mathf.Abs(_rigidbody.velocity.x) > x_Max_Velocity) && _rigidbody.velocity.x != 0)
                {
                    playerVelocity = _rigidbody.velocity + new Vector2((((_rigidbody.velocity.x > 0) ? -x_basicLimitDecreaseRate : x_basicLimitDecreaseRate) + MoveDir.normalized.x * x_limitDecreaseRate) * Time.deltaTime, 0);
                }
                if (OnGround || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    JustReleaseHook = false;
                }
                // 松开钩子没有操作时
                if (JustReleaseHook && _rigidbody.velocity.x != 0)
                {
                    playerVelocity = _rigidbody.velocity + new Vector2(((_rigidbody.velocity.x > 0) ? -x_decreaseRateWithoutControl : x_decreaseRateWithoutControl) * Time.deltaTime, 0);
                }
                
                // y 直接限制最大速度
                if (_rigidbody.velocity.y <= -y_Max_Velocity)
                {
                    _rigidbody.AddForce(new Vector2(0, 9.81f * 5));
                    playerVelocity = new Vector2(playerVelocity.x, -y_Max_Velocity);
                    
                }
            }
            _rigidbody.velocity = playerVelocity;
        }
        
    }
    private void MoveTimer()
    {
        deltaMoveDir = MoveDir - lastMoveDir;

        if (StartMoveTime == 0 || EndMoveTime == 0)
        {
            Debug.LogError("StartMoveTime and EndMoveTime cannot be 0!");
            return;
        }
        
        if (deltaMoveDir.x >= 0 && MoveDir.x >= 1)
        {
            if (moveTimer < 1 && moveTimer >= 0) // 向右起步
                moveTimer += Time.fixedDeltaTime / StartMoveTime;
            else // 向右调头
                moveTimer += Time.fixedDeltaTime / EndMoveTime;
        }
        
        else if (deltaMoveDir.x <= 0 && MoveDir.x <= -1)
        {
            if (moveTimer > -1 && moveTimer <= 0) // 向左起步
                moveTimer -= Time.fixedDeltaTime / StartMoveTime;
            else // 向左调头
                moveTimer -= Time.fixedDeltaTime / EndMoveTime;
        }
        // 停止阶段
        else if (moveTimer != 0)
        {
            // 向右停止
            if (deltaMoveDir.x <= 0 && MoveDir.x > 0)
            {
                if (moveTimer > 0)
                    moveTimer -= Time.fixedDeltaTime / EndMoveTime;
                moveTimer = Mathf.Clamp(moveTimer, 0, 1);
            }
            // 向左停止
            else if (deltaMoveDir.x >= 0 && MoveDir.x < 0)
            {
                if (moveTimer < 0)
                    moveTimer += Time.fixedDeltaTime / EndMoveTime;
                moveTimer = Mathf.Clamp(moveTimer, -1, 0);
            }
        }
        if (MoveDir == Vector2.zero && deltaMoveDir == Vector2.zero)
            moveTimer = 0;
        moveTimer = Mathf.Clamp(moveTimer, -1, 1);
        lastMoveDir = MoveDir;
    }

    public bool Jump()
    {
        delta_JumpInteraction = PlayerGameInput.GetJumpInteraction() - lastJumpInteraction;
        if (OnGround)
        {
            _rigidbody.gravityScale = oriGravity;
        }
        else
        {
            _rigidbody.gravityScale = GravityCurve.Evaluate(Mathf.Abs(_rigidbody.velocity.y) / JumpSpeed) * oriGravity;
        }
        // 跳跃控制
        // 土狼时间（当OnGround为false时，给予缓冲）
        if (just_OnGround)
        {
            startCoyoteTimer = true;
        }
        if (coyoteTimer != 0 && delta_JumpInteraction > 0)
        {
            Debug.Log("Coyote Time");
            jumpInteraction = true;
        }
        // 触地判断缓冲（当delta_JumpInteraction > 0时，给予缓冲判断是否OnGround）
        if (delta_JumpInteraction > 0)
        {
            startJumpBufferTimer = true;
        }
        if (jumpBufferTimer != 0 && OnGround)
        {
            // Debug.Log("Jump Buffer");
            jumpInteraction = true;
        }
        // 执行跳跃动作
        if (jumpInteraction)
        {
            if (jumpInteraction_CDTimer == 0)
            {
                JumpAction();
                startjumpInteraction_CDTimer = true;
            }
            jumpInteraction = false;
            
            lastJumpInteraction = PlayerGameInput.GetJumpInteraction();
            return true;
        }
        lastJumpInteraction = PlayerGameInput.GetJumpInteraction();
        return false;
    }
    private void CoyoteTimer()
    {
        if (startCoyoteTimer)
        {
            coyoteTimer++;
            if (coyoteTimer >= 0.2f / Time.fixedDeltaTime)
            {
                coyoteTimer = 0;
                startCoyoteTimer = false;
            }
        }
    }
    private void JumpBufferTimer()
    {
        if (startJumpBufferTimer)
        {
            jumpBufferTimer++;
            if (jumpBufferTimer >= 0.1f / Time.fixedDeltaTime)
            {
                jumpBufferTimer = 0;
                startJumpBufferTimer = false;
            }
        }
    }
    private void JumpInteraction_CDTimer()
    {
        if (startjumpInteraction_CDTimer)
        {
            jumpInteraction_CDTimer++;
            if (jumpInteraction_CDTimer >= 0.25f / Time.fixedDeltaTime)
            {
                jumpInteraction_CDTimer = 0;
                startjumpInteraction_CDTimer = false;
            }
        }
    }

    private void IsOnGround()
    {
        // Debug.Log("IsOnGround()");
        if (_rigidbody.velocity.y > 0.1f)
        {
            OnGround = false;
            lastOnGround = false;
            return;
        }
        Collider2D[] cols = Physics2D.OverlapBoxAll((Vector2)transform.position + OnGroundColliderCenter,
            OnGroundColliderSize, -_transform.rotation.z, 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("OneWayGround"));
        if (cols.Length != 0)
        {
            OnGround = true;
        }
        else
        {
            OnGround = false;
        }
        just_OnGround = (lastOnGround == true && OnGround == false) ? true : false;
        // Debug.Log("LastOnGround:" + lastOnGround + "     OnGround:" + OnGround);
        lastOnGround = OnGround;
    }
    private void JumpAction()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        _rigidbody.velocity += new Vector2(0, JumpSpeed);
    }
    private void CarryNPC()
    {
        if (carriedNPC == null) return;
        if (PlayerGameInput.GetThrowInteraction() > 0.9f && canCarryNPC && !allowThrowCube)
        {
            carriedNPC.GetComponent<Rigidbody2D>().gravityScale = 0;
            carriedNPC.transform.position = _transform.position + new Vector3((FaceRight ? (-offset_NpcToPlayer) : offset_NpcToPlayer), 0, 0);
        }
        else
        {
            carriedNPC.GetComponent<Rigidbody2D>().gravityScale = 6;
        }
    }
    private void ThrowCube()
    {
        delta_ThrowInteraction = PlayerGameInput.GetThrowInteraction() - lastThrowInteraction;
        Vector2 ArrowDir = PlayerGameInput.GetArrowDir();
        if (PlayerGameInput.GetThrowInteraction() > 0.9f && canThrowCube)
        {
            Time.timeScale = 0.1f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            onBulletTime = true;
            Bullet_Timer++;
            Arrow.SetActive(true);
            if (ArrowDir != Vector2.zero)
                Arrow.transform.up = ArrowDir;
            else if (FaceRight)
                Arrow.transform.up = Vector2.right;
            else
                Arrow.transform.up = Vector2.left;
        }
        if ((delta_ThrowInteraction < 0 || Bullet_Timer >= 150) && canThrowCube)
        {
            currentCube = Instantiate(CubePrefab, ThrowPos.position, Quaternion.identity).GetComponent<Cube>();
            currentCube.Player = gameObject;
            Rigidbody2D cube_rigidbody = currentCube.gameObject.GetComponent<Rigidbody2D>();
            if (cube_rigidbody != null)
            {
                if (ArrowDir != Vector2.zero)
                    cube_rigidbody.AddForce(ArrowDir * ThrowForce, ForceMode2D.Impulse);
                else if (FaceRight)
                    cube_rigidbody.AddForce(Vector2.right * ThrowForce, ForceMode2D.Impulse);
                else
                    cube_rigidbody.AddForce(Vector2.left * ThrowForce, ForceMode2D.Impulse);
            }
            CubeCDTimer = 0;
            canThrowCube = false;
            Bullet_Timer = 0;
            Arrow.SetActive(false);
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            onBulletTime = false;
        }
        lastThrowInteraction = PlayerGameInput.GetThrowInteraction();
    }
    private void CubeTimer()
    {
        if (CubeCDTimer < 150)
            CubeCDTimer++;
        if (CubeCDTimer == 150 && FlashOver)
            canThrowCube = true;
        else
            canThrowCube = false;
    }
    private void CheckCubeCD()
    {
        Color Transparent = new Color(1, 1, 1, 0.5f);
        Color white = new Color(1, 1, 1, 1f);
        if (CubeCDTimer < 150)
        {
            sprite.color = Transparent;
        }
        else
        {
            sprite.color = white;
        }
    }
    private bool Flash()
    {
        if (!canThrowCube && currentCube != null && CubeCDTimer > 10)
        {
            if (delta_ThrowInteraction > 0)
            {
                FlashOver = false;
                // Effect
                if (FlashEffect != null)
                {
                    trailRenderer.emitting = true;
                    Instantiate(FlashEffect, transform.position, Quaternion.identity);
                }
                // 什么都没有击中
                if (!currentCube.HitEnemy && !currentCube.HitInteractiveItem && !currentCube.HitGround)
                {
                    gameObject.transform.position = currentCube.gameObject.transform.position;
                    Destroy(currentCube.gameObject);
                    _rigidbody.velocity = Vector2.zero;
                }
                // 击中墙壁，自动跳起
                if (currentCube.HitGround)
                {
                    gameObject.transform.position = currentCube.gameObject.transform.position;
                    CubeCDTimer = 140;
                    JumpAction();
                    // 进入无敌帧
                    isInvincible = true;
                    Invoke("CancelInvincible", 0.04f);
                    Destroy(currentCube.gameObject);
                }
                // 击中敌人，自动跳起
                if (currentCube.HitEnemy)
                {
                    currentCube.target.GetComponent<EnemyUnit>().GetHurt(1);
                    gameObject.transform.position = currentCube.gameObject.transform.position;
                    CubeCDTimer = 140;
                    JumpAction();
                    Destroy(currentCube.gameObject);
                }
                // 击中可交互物体
                if (currentCube.HitInteractiveItem)
                {
                    currentCube.InteractiveItem.GetComponent<InterActiveItem>().InterAction();
                    gameObject.transform.position = currentCube.gameObject.transform.position;
                    CubeCDTimer = 140;
                    JumpAction();
                    Destroy(currentCube.gameObject);
                }
                if (FlashEffect != null)
                {
                    Invoke("SetTrailRendererFalse", 0.1f);
                    Instantiate(FlashEffect, transform.position, Quaternion.identity);
                }
                SecJump = false;
                return true;
            }
        }
        if (delta_ThrowInteraction < 0)
            FlashOver = true;
        return false;
    }
    /// <summary>
    /// 取消无敌帧
    /// </summary>
    private void CancelInvincible()
    {
        isInvincible = false;
    }
    private void SetTrailRendererFalse()
    {
        trailRenderer.emitting = false;
    }
    private void GetHook()
    {
        delta_HookInteraction = PlayerGameInput.GetHookInteraction() - lastHookInteraction;
        nearestDistance = HookCircleRadius;
        if (Hook_CD_Time < 150)
            Hook_CD_Time++;
        if (!onHook)
            Rope.gameObject.SetActive(false);
        if (!OnGround && !onHook && Hook_CD_Time >= 150)
        {
            if (NearestHook != null)
            {
                float Distance_nearestHook_Player = Vector2.Distance(NearestHook.transform.position, transform.position);
                if (Distance_nearestHook_Player > HookCircleRadius)
                    NearestHook = null;
            }
            NearestHook = null;
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, HookCircleRadius, 1 << LayerMask.NameToLayer("Hook"));
            if (cols.Length != 0)
            {
                foreach (var col in cols)
                {
                    float currentDistance = Vector2.Distance(col.gameObject.transform.position, transform.position);
                    if ((FaceRight && col.gameObject.transform.position.x < _transform.position.x) ||
                        (!FaceRight && col.gameObject.transform.position.x > _transform.position.x) ||
                        (col.gameObject.transform.position.y <= _transform.position.y) ||
                        (currentDistance < Min_RopeLength))
                        continue;
                    if (nearestDistance > currentDistance)
                    {
                        NearestHook = col.gameObject;
                        nearestDistance = currentDistance;
                    }
                }
                if (NearestHook == null)
                {
                    return;
                }
                if (delta_HookInteraction > 0)
                {
                    onHook = true;
                    SecJump = false;
                    velocity_beforeHook = _rigidbody.velocity;
                    justHook = true;
                    NearestHook.GetComponent<HingeJoint2D>().connectedBody = _rigidbody;
                    /*Vector2 destination = NearestHook.transform.position;
				    currentHook = (GameObject)Instantiate (hookPrefab, transform.position, Quaternion.identity);
				    currentHook.GetComponent<Rope>().player = gameObject;
				    currentHook.GetComponent<Rope>().destination = destination;*/
                }
            }
        }
        if (onHook && NearestHook != null)
        {
            Hang_Time++;
            // // 上下攀爬
            Vector2 player_hook_dir = (NearestHook.transform.position - transform.position);
            if (Input.GetKey(KeyCode.W) && transform.position.y < NearestHook.transform.position.y && player_hook_dir.magnitude > Min_RopeLength)
                transform.Translate(player_hook_dir.normalized * ClimbSpeed * Time.deltaTime);
            if (Input.GetKey(KeyCode.S) && transform.position.y < NearestHook.transform.position.y && player_hook_dir.magnitude < Max_RopeLength)
                transform.Translate(-player_hook_dir.normalized * ClimbSpeed * Time.deltaTime);
            // // 左右晃动
            if (transform.position.y < NearestHook.transform.position.y)
            {
                Vector2 swingDir = new Vector2(MoveDir.x, 0);
                _rigidbody.AddForce(swingDir * SwingForce * Time.deltaTime, ForceMode2D.Force);
            }

            // 断开连接 (跳跃断开判定有点奇怪)
            if (delta_HookInteraction < 0 || OnGround || this.Flash())
            {
                onHook = false;
                NearestHook.GetComponent<HingeJoint2D>().connectedBody = null;
                // Destroy (currentHook);
                Hang_Time = 0;
                NearestHook = null;
                JustReleaseHook = true;
            }
            if (Hang_Time >= 99999)
            {
                onHook = false;
                NearestHook.GetComponent<HingeJoint2D>().connectedBody = null;
                // Destroy (currentHook);
                Hang_Time = 0;
                Hook_CD_Time = 0;
                NearestHook = null;
                JustReleaseHook = true;
            }
        }
        // 可视化Rope
        if (onHook && NearestHook != null)
        {
            Rope.gameObject.SetActive(true);
            Rope.SetPosition(0, transform.position);
            Rope.SetPosition(1, NearestHook.transform.position);
        }
        lastHookInteraction = PlayerGameInput.GetHookInteraction();
    }
    private void OnHook()
    {
        if (onHook)
        {
            _rigidbody.freezeRotation = false;
            _rigidbody.gravityScale = oriGravity / 2;
            if (justHook)
            {
                _rigidbody.velocity = velocity_beforeHook;
                justHook = false;
            }
            transform.up = NearestHook.transform.position - transform.position;
        }
    }

    /// <summary>
    /// 重置角色控制器状态
    /// </summary>
    public void ResetPlayerControllerStatus()
    {
        delta_HookInteraction = 0;
        delta_ThrowInteraction = 0;
        delta_JumpInteraction = 0;
        lastJumpInteraction = 0;
        lastThrowInteraction = 0;
        lastHookInteraction = 0;

        onBulletTime = false;
        onHook = false;

        MoveDir = Vector2.zero;

        CubeCDTimer = 150;
        Bullet_Timer = 0;

        FlashOver = true;

        Hang_Time = 0;
        Hook_CD_Time = 150;

        Arrow.SetActive(false);
        Rope.gameObject.SetActive(false);

        if (NearestHook != null)
        {
            NearestHook.GetComponent<HingeJoint2D>().connectedBody = null;
            NearestHook = null;
        }
    }
    public void SwitchGameInput()
    {
        switch (dropdown.value)
        {
            case 0: PlayerGameInput = gameObject.GetComponent<PC_Input>(); break;
            case 1: PlayerGameInput = gameObject.GetComponent<XBox_Input>(); break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawWireCube((Vector2)transform.position + OnGroundColliderCenter, OnGroundColliderSize);
        
        Gizmos.DrawWireSphere(transform.position, HookCircleRadius);
    }
}