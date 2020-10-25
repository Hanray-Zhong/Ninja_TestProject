using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffect : MonoBehaviour
{
    [Header("是否开启残影效果")]
    public bool OpenGhoseEffect;

    [Header("是否开启褪色消失")]
    public bool OpenFade;


    [Header("显示残影的持续时间")]
    public float DurationTime;
    [Header("生成残影与残影之间的时间间隔")]
    public float SpawnTimeval;
    private float SpawnTimer;//生成残影的时间计时器
    [Header("闪现残影的间距")]
    public float FlashGhostSpacing;
    [Header("闪现残影的持续时间")]
    public float FlashGhostDurationTime;

    [Header("残影颜色")]
    public Color GhostColor;
    [Header("残影层级")]
    public int GhostSortingOrder;

    private PlayerController playerController;
    private SpriteRenderer playerSR;
    private List<GameObject> ghostList = new List<GameObject>();
    private List<GameObject> flashGhostList = new List<GameObject>();


    private void Awake()
    {
        playerController = PlayerController.Instance;
        playerSR = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Fade();

        if (OpenGhoseEffect == false)
        {
            return;
        }

        DrawGhost();
    }

    /// <summary>
    /// 绘制残影
    /// </summary>
    private void DrawGhost()
    {
        if (SpawnTimer >= SpawnTimeval)
        {
            SpawnTimer = 0;

            GameObject ghost = new GameObject();
            ghostList.Add(ghost);
            ghost.name = "ghost";
            ghost.AddComponent<SpriteRenderer>();
            ghost.transform.position = transform.position;
            ghost.transform.localScale = transform.localScale;
            SpriteRenderer sr = ghost.GetComponent<SpriteRenderer>();
            sr.sprite = playerSR.sprite;
            sr.sortingOrder = GhostSortingOrder;
            sr.color = GhostColor;
            sr.flipX = playerController.FaceRight ? false : true;

            if (OpenFade == false)
            {
                Destroy(ghost, DurationTime);
            }
        }
        else
        {
            SpawnTimer += Time.deltaTime;
        }
    }

    /// <summary>
    /// 褪色操作
    /// </summary>
    private void Fade()
    {
        if (OpenFade == false && ghostList.Count == 0 && flashGhostList.Count == 0)
        {
            return;
        }
        // 普通残影
        for (int i = 0; i < ghostList.Count; i++)
        {
            SpriteRenderer ghostSR = ghostList[i].GetComponent<SpriteRenderer>();
            if (ghostSR.color.a <= 0)
            {
                GameObject tempGhost = ghostList[i];
                ghostList.Remove(tempGhost);
                Destroy(tempGhost);
            }
            else
            {
                float fadePerSecond = GhostColor.a / DurationTime;
                Color tempColor = ghostSR.color;
                tempColor.a -= fadePerSecond * Time.deltaTime;
                ghostSR.color = tempColor;
            }
        }
        // 闪现产生的残影
        for (int i = 0; i < flashGhostList.Count; i++)
        {
            SpriteRenderer ghostSR = flashGhostList[i].GetComponent<SpriteRenderer>();
            if (ghostSR.color.a <= 0)
            {
                GameObject tempGhost = flashGhostList[i];
                flashGhostList.Remove(tempGhost);
                Destroy(tempGhost);
            }
            else
            {
                float fadePerSecond = GhostColor.a / FlashGhostDurationTime;
                Color tempColor = ghostSR.color;
                tempColor.a -= fadePerSecond * Time.deltaTime;
                ghostSR.color = tempColor;
            }
        }
    }

    /// <summary>
    /// 针对闪现实现的残影
    /// </summary>
    public void DrawFlashGhost(Vector2 start, Vector2 end)
    {
        if (FlashGhostSpacing == 0)
        {
            Debug.LogError("GhostSpacing cannot be 0.");
            return;
        }
        int ghostNum = (int)(Vector2.Distance(start, end) / FlashGhostSpacing);
        float x_offset = (end.x - start.x) / ghostNum;
        float y_offset = (end.y - start.y) / ghostNum;

        for (int i = 0; i < ghostNum; i++)
        {
            GameObject ghost = new GameObject();
            flashGhostList.Add(ghost);
            ghost.name = "ghost";
            ghost.AddComponent<SpriteRenderer>();
            ghost.transform.position = start + new Vector2(i * x_offset, i * y_offset);
            ghost.transform.localScale = transform.localScale;
            SpriteRenderer sr = ghost.GetComponent<SpriteRenderer>();
            sr.sprite = playerSR.sprite;
            sr.sortingOrder = GhostSortingOrder;
            sr.color = new Color(GhostColor.r, GhostColor.g, GhostColor.b, (float)(decimal)i / ghostNum);
            sr.flipX = playerController.FaceRight ? false : true;
        }
    }
}
