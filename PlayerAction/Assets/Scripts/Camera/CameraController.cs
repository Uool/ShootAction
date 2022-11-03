using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    private void Start()
    {
        SetCamera();
    }

    public void SetCamera()
    {
        //target = FindObjectOfType<PlayerController>().transform;
        offset = transform.position - target.position;
    }
    // Todo : �̰� �����¸� �����ų��.. �ƴϸ� Ÿ �ٸ��Ϳ��� �����ų��..
    public void FollowTarget()
    {
        transform.LookAt(target);
        transform.position = target.position + offset;
    }

    private void FixedUpdate()
    {
        FollowTarget();
    }

    // ���ͺ�
    public void SetQuaterView()
    {

    }

    // 3��Ī
    public void SetThirdPersonView()
    {

    }
    
}
