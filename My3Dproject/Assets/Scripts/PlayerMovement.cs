using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed, jumpForce;

    float hAxis, vAxis;

    public bool wDown, jDown, isJump;

    public Vector3 moveVec;

    Rigidbody rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
        Move();
        Jump();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal"); //x방향 입력 (키보드 a,d)
        vAxis = Input.GetAxisRaw("Vertical"); //z방향 입력 (키보드 w,s)
        wDown = Input.GetButton("Walk"); //걷기 입력 (왼쪽 shift)
        jDown = Input.GetButton("Jump"); //점프 입력
    }

    void Move()
    {
        //백터 생성, 대각선 이동때도 같은 값 가지기 위해 nomalized사용
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (wDown) //wDown == true라면
            transform.position += moveVec * speed * 0.3f * Time.deltaTime; //0.3을 곱함(더 느리게)
        else //wDown == false라면
            transform.position += moveVec * speed * Time.deltaTime; //백터 * 스피드 * 델타타임을 위치에 계속 더해줌

        transform.LookAt(transform.position + moveVec);
    }

    void Jump()
    {
        if (jDown && !isJump)
        {
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJump = true;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
            isJump = false;
        }
    }
}
