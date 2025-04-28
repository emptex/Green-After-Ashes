using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerSceneLoader : MonoBehaviour
{
    [Tooltip("要加载的场景名称，确保已添加到 Build Settings")]
    public string sceneName;

    [Tooltip("玩家对象的 Tag")]
    public string playerTag = "Player";

    private bool hasLoaded = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[TriggerSceneLoader] OnTriggerEnter with {other.name}");
        if (hasLoaded) return;

        if (other.CompareTag(playerTag))
        {
            hasLoaded = true;
            Debug.Log($"[TriggerSceneLoader] 玩家进入 Trigger，加载场景：{sceneName}");
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log($"[TriggerSceneLoader] Tag 不匹配：{other.tag}");
        }
    }
}