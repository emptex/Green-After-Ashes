using UnityEngine;
using UnityEngine.UI;

public class AutoDeactivateOnAlphaZero : MonoBehaviour
{
    private Image image;
    private SpriteRenderer spriteRenderer;

    [Tooltip("Alpha阈值，小于这个值就关闭物体")]
    public float alphaThreshold = 0.01f;

    private void Awake()
    {
        image = GetComponent<Image>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float alpha = 1f;

        if (image != null)
        {
            alpha = image.color.a;
        }
        else if (spriteRenderer != null)
        {
            alpha = spriteRenderer.color.a;
        }
        else
        {
            // 如果身上啥也没有，直接停掉这个脚本
            enabled = false;
            return;
        }

        if (alpha <= alphaThreshold)
        {
            gameObject.SetActive(false);
        }
    }
}