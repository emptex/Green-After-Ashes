using UnityEngine;

public class CameraFollowFixedRotation : MonoBehaviour
{
    [Tooltip("跟随的目标对象（一般是玩家）")]
    public Transform target;

    [Tooltip("相对于目标的偏移（例如 new Vector3(0, 5, -10)）")]
    public Vector3 positionOffset = new Vector3(0, 5f, -10f);

    [Tooltip("锁定的世界旋转角度（例如 new Vector3(30, 0, 0)）")]
    public Vector3 fixedRotationEuler = new Vector3(30f, 0f, 0f);

    void LateUpdate()
    {
        if (target != null)
        {
            // 保持固定偏移跟随目标位置
            transform.position = target.position + positionOffset;

            // 锁定世界角度旋转
            transform.rotation = Quaternion.Euler(fixedRotationEuler);
        }
    }
}