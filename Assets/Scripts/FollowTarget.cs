using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour
{

    public float smooth = 3;                // 摄像机移动平滑指数

    private Vector3 targetPosition;     // the position the camera is trying to be in
   
    public Transform follow;

    private Vector3 distanceVector;

    void Start()
    {
        transform.LookAt(follow);
        distanceVector = transform.position - follow.position;
    }

    void LateUpdate()
    {
        // 设置追踪目标的坐标作为调整摄像机的偏移量
        targetPosition = follow.position + distanceVector;

        // 在摄像机和被追踪物体之间制造一个顺滑的变化
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
    }
}
