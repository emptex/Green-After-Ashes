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
            ActivateObjects();
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
}