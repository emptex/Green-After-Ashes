using UnityEngine;

public class CollisionTriggerActivator : MonoBehaviour
{
    [Header("Collision Settings")]
    [Tooltip("检测碰撞物体的 Tag")]
    public string targetTag;

    [Header("Objects To Activate")]
    [Tooltip("碰撞后需要激活的物体列表")]
    public GameObject[] objectsToActivate;

    [Header("Objects To Deactivate")]
    [Tooltip("碰撞后需要关闭的物体列表")]
    public GameObject[] objectsToDeactivate;

    [Header("Animator Trigger Settings")]
    [Tooltip("指定的 Animator")]
    public Animator targetAnimator;

    [Tooltip("要触发的 Trigger 名称")]
    public string triggerName;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;

        if (other.CompareTag(targetTag))
        {
            hasTriggered = true;
            ActivateObjects();
            DeactivateObjects();
            TriggerAnimation();
        }
    }

    private void ActivateObjects()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            if (obj != null)
                obj.SetActive(true);
        }
    }

    private void DeactivateObjects()
    {
        foreach (GameObject obj in objectsToDeactivate)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }

    private void TriggerAnimation()
    {
        if (targetAnimator != null && !string.IsNullOrEmpty(triggerName))
        {
            targetAnimator.SetTrigger(triggerName);
        }
    }
}