using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("玩家物体的Tag")]
    public string playerTag = "Player";

    [Tooltip("进入检测范围时显示的提示物体")]
    public GameObject hintObject;

    [Tooltip("按下交互键时激活的物体列表")]
    public GameObject[] objectsToActivate;

    [Tooltip("按下交互键时关闭的物体列表")]
    public GameObject[] objectsToDeactivate;

    [Tooltip("必须激活状态的指定检测器（为空则不检测）")]
    public GameObject requiredDetector;

    private bool isPlayerInRange = false;
    private bool hasInteracted = false;

    private void Start()
    {
        if (hintObject != null)
            hintObject.SetActive(false);
    }

    private void Update()
    {
        if (!hasInteracted && isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // 如果设置了检测器且当前未激活，则不执行交互
            if (requiredDetector != null && !requiredDetector.activeInHierarchy)
                return;

            ActivateObjects();
            DeactivateObjects();

            if (hintObject != null)
                hintObject.SetActive(false);

            hasInteracted = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasInteracted) return;

        if (other.CompareTag(playerTag))
        {
            isPlayerInRange = true;

            if (hintObject != null)
                hintObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (hasInteracted) return;

        if (other.CompareTag(playerTag))
        {
            isPlayerInRange = false;

            if (hintObject != null)
                hintObject.SetActive(false);
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
}