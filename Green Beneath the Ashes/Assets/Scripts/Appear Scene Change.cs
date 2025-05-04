using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AlphaSceneSwitcher : MonoBehaviour
{
    [Tooltip("目标场景名称（需添加到 Build Settings）")]
    public string targetSceneName;

    [Tooltip("是否使用 UI Image 组件读取 Alpha（否则使用 CanvasGroup）")]
    public bool useUIImage = false;

    private CanvasGroup canvasGroup;
    private Image uiImage;
    private bool hasTriggered = false;

    private void Start()
    {
        if (useUIImage)
        {
            uiImage = GetComponent<Image>();
            if (uiImage == null)
                Debug.LogWarning("Missing Image component for alpha check.");
        }
        else
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
                Debug.LogWarning("Missing CanvasGroup component for alpha check.");
        }
    }

    private void Update()
    {
        if (hasTriggered) return;

        float currentAlpha = useUIImage && uiImage != null
            ? uiImage.color.a
            : canvasGroup != null ? canvasGroup.alpha : 0f;

        if (currentAlpha >= 1f)
        {
            hasTriggered = true;
            SceneManager.LoadScene(targetSceneName);
        }
    }
}