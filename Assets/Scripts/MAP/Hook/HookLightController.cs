using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class HookLightController : MonoBehaviour
{
    public Light2D HookLight;
    public float HookCircleRadius;

    private PlayerController playerController;
    private Color redColor = new Color(1, 0, 0);
    private Color yellowColor = new Color(1, 1, 0);
    private Color greenColor = new Color(0, 1, 0);

    private void Awake()
    {
        playerController = PlayerController.Instance;
    }

    private void Update()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, HookCircleRadius, 1 << LayerMask.NameToLayer("Player"));
        if (player != null)
        {
            if (playerController.onHook && playerController.currentHook == gameObject)
            {
                HookLight.color = greenColor;
            }
            else if (playerController.nearestHook == gameObject)
            {
                HookLight.color = yellowColor;
            }
            else
            {
                HookLight.color = redColor;
            }
        }
        else
        {
            HookLight.color = redColor;
        }
    }
}
