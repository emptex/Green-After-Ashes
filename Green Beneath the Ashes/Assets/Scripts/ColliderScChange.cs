using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionLoadScene : MonoBehaviour
{
    [Header("Scene Settings")]
    [Tooltip("要切换到的场景名字")]
    public string sceneName;

    [Tooltip("碰撞后等待的时间")]
    public float delaySeconds = 2f;

    private bool hasCollided = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasCollided)
        {
            hasCollided = true;
            Invoke(nameof(LoadScene), delaySeconds);
        }
    }

    private void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}