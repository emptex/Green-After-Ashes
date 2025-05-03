using UnityEngine;

public class TriggerActivatorWithGate : MonoBehaviour
{
    [Header("目标物体")]
    [Tooltip("需要在接触时开启、离开时关闭的物体列表")]
    public GameObject[] targetObjects;

    [Header("玩家识别")]
    [Tooltip("玩家物体的Tag")]
    public string playerTag = "Player";

    [Header("状态检测器")]
    [Tooltip("只有当这个物体为激活状态时，才允许触发器工作")]
    public GameObject gateChecker;

    private void OnTriggerEnter(Collider other)
    {
        if (IsGateActive() && other.CompareTag(playerTag))
        {
            SetObjectsActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsGateActive() && other.CompareTag(playerTag))
        {
            SetObjectsActive(false);
        }
    }

    private bool IsGateActive()
    {
        // 如果未指定 gateChecker，默认视为允许
        return gateChecker == null || gateChecker.activeSelf;
    }

    private void SetObjectsActive(bool isActive)
    {
        foreach (GameObject obj in targetObjects)
        {
            if (obj != null)
                obj.SetActive(isActive);
        }
    }
}