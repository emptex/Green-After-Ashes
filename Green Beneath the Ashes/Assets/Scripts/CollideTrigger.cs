using UnityEngine;

public class CollisionTriggerActivator : MonoBehaviour
{
    [Header("Collision Settings")]
    [Tooltip("检测碰撞物体的Tag")]
    public string targetTag;

    [Header("Objects To Activate")]
    [Tooltip("碰撞后需要激活的物体列表")]
    public GameObject[] objectsToActivate;

    [Header("Animator Trigger Settings")]
    [Tooltip("指定的Animator")]
    public Animator targetAnimator;

    [Tooltip("要触发的Trigger名称")]
    public string triggerName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            ActivateObjects();
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

    private void TriggerAnimation()
    {
        if (targetAnimator != null && !string.IsNullOrEmpty(triggerName))
        {
            targetAnimator.SetTrigger(triggerName);
        }
    }
}