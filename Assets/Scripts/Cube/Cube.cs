using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour
{
    public float ActiveTime;
    public float rotateSpeed;

    [Header("Hit")]
    public bool HitGround = false;
    public Vector3 HitGroundFlashPos { get; set; }
    public float HitGroundFlashPosOffset;
    public bool HitEnemy = false;
    public bool HitBoss = false;
    public GameObject target = null;
    public bool HitInteractiveItem = false;
    public GameObject InteractiveItem;
    public float timeScale;

    [Header("∑…Ô⁄≈–∂®«¯”Ú")]
    public float CheckGroundRadius;
    public float CheckOverlapWidth;
    public float CheckOverlapHeight;


    private PlayerUnit playerUnit;
    private Rigidbody2D cube_rigidbody;
    private Transform cube_transform;

    private void Awake()
    {
        playerUnit = PlayerUnit.Instance;
        cube_rigidbody = gameObject.GetComponent<Rigidbody2D>();
        cube_transform = gameObject.GetComponent<Transform>();
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime, Space.World);
        CheckGround();
        StartCoroutine(Disappear());
    }

    private void CheckGround()
    {
        Collider2D groundCol_up = Physics2D.OverlapBox((Vector2)cube_transform.position + new Vector2(0, CheckGroundRadius), new Vector2(CheckOverlapWidth, CheckOverlapHeight), 0, 1 << LayerMask.NameToLayer("Ground"));
        Collider2D groundCol_down = Physics2D.OverlapBox((Vector2)cube_transform.position + new Vector2(0, -CheckGroundRadius), new Vector2(CheckOverlapWidth, CheckOverlapHeight), 0, 1 << LayerMask.NameToLayer("Ground"));
        Collider2D groundCol_left = Physics2D.OverlapBox((Vector2)cube_transform.position + new Vector2(-CheckGroundRadius, 0), new Vector2(CheckOverlapHeight, CheckOverlapWidth), 0, 1 << LayerMask.NameToLayer("Ground"));
        Collider2D groundCol_right = Physics2D.OverlapBox((Vector2)cube_transform.position + new Vector2(CheckGroundRadius, 0), new Vector2(CheckOverlapHeight, CheckOverlapWidth), 0, 1 << LayerMask.NameToLayer("Ground"));
        Vector4 isHitGround = new Vector4((groundCol_up == null) ? 0 : 1, (groundCol_down == null) ? 0 : 1, (groundCol_left == null) ? 0 : 1, (groundCol_right == null) ? 0 : 1);
        if (isHitGround != Vector4.zero)
        {
            // Debug.Log(groundCol_up.name);
            HitGround = true;
            HitGroundFlashPos = transform.position + new Vector3(HitGroundFlashPosOffset, 0) * isHitGround.z + new Vector3(-HitGroundFlashPosOffset, 0) * isHitGround.w;
            cube_rigidbody.velocity = Vector2.zero;
            Destroy(gameObject, 1);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (playerUnit.IsDead)
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            Destroy(gameObject);
            return;
        }
        if (other.tag == "Enemy" || other.tag == "Boss")
        {
            HitEnemy = true;
            target = other.gameObject;
            target.GetComponent<Animator>().SetBool("isGetCube", true);
            PlayerSoundController.Instance.Play(PlayerSoundType.stoptime);
            Time.timeScale = timeScale;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        if (other.tag == "InterActiveItem")
        {
            HitInteractiveItem = true;
            InteractiveItem = other.gameObject;
            PlayerSoundController.Instance.Play(PlayerSoundType.stoptime);
            Time.timeScale = timeScale;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy" || other.tag == "Boss")
        {
            target = other.gameObject;
            target.GetComponent<Animator>().SetBool("isGetCube", false);
        }
        HitEnemy = false;
        HitInteractiveItem = false;
        PlayerSoundController.Instance.StopPlay(PlayerSoundType.stoptime);
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(ActiveTime);
        cube_rigidbody.velocity = Vector2.zero;
        Destroy(gameObject, 1);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawWireSphere(transform.position, HitGroundFlashPosOffset);
        // up
        Gizmos.DrawWireCube(transform.position + new Vector3(0, CheckGroundRadius, 0), new Vector3(CheckOverlapWidth, CheckOverlapHeight));
        // down
        Gizmos.DrawWireCube(transform.position + new Vector3(0, -CheckGroundRadius, 0), new Vector3(CheckOverlapWidth, CheckOverlapHeight));
        // left
        Gizmos.DrawWireCube(transform.position + new Vector3(-CheckGroundRadius, 0, 0), new Vector3(CheckOverlapHeight, CheckOverlapWidth));
        // right
        Gizmos.DrawWireCube(transform.position + new Vector3(CheckGroundRadius, 0, 0), new Vector3(CheckOverlapHeight, CheckOverlapWidth));
    }
}