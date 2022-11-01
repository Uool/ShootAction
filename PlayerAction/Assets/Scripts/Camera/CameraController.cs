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
    // Todo : �̰� �����¸� �����ų��.. �ƴϸ� Ÿ �ٸ��Ϳ��� �����ų��..
    public void FollowTarget()
    {
        transform.LookAt(target);
        transform.position = target.position + offset;
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
