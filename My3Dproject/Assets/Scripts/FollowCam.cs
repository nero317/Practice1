using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset; //카메라의 위치 = target의 위치 + 카메라의 초기 위치
    }
}
