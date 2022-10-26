using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 offset;
    private Transform target;

    public void SetCamera()
    {
        target = FindObjectOfType<PlayerController>().transform;
        offset = transform.position - target.position;
    }
    // Todo : 이걸 나한태만 적용시킬지.. 아니면 타 다른것에도 적용시킬지..
    public void FollowTarget()
    {
        transform.LookAt(target);
        transform.position = target.position + offset;
    }

    // 쿼터뷰
    public void SetQuaterView()
    {

    }

    // 3인칭
    public void SetThirdPersonView()
    {

    }
    
}
