using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed, jumpForce;
    float hAxis, vAxis;

    public bool jDown, isJump, isDodge, iDown;
    public bool sDown1, sDown2, sDown3, isSwap;

    public Vector3 moveVec, dodgeVec;

    public Animator anim;

    PlayerItem pi;

    Rigidbody rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        pi = GetComponent<PlayerItem>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        GetInput();
        Move();
        Jump();
        Dodge();
        pi.Interaction();
        pi.Swap();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal"); //x방향 입력 (키보드 a,d)
        vAxis = Input.GetAxisRaw("Vertical"); //z방향 입력 (키보드 w,s)
        jDown = Input.GetButtonDown("Jump"); //점프 입력
        iDown = Input.GetButtonDown("Interaction"); //상호작용 키 입력
        sDown1 = Input.GetButtonDown("Swap1"); //무기 스왑키 1, 2, 3입력
        sDown2 = Input.GetButtonDown("Swap2");
        sDown3 = Input.GetButtonDown("Swap3");
    }

    void Move()
    {
        //백터 생성, 대각선 이동때도 같은 값 가지기 위해 nomalized사용
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isDodge) //회피중일경우
            moveVec = dodgeVec; //moveVec를 dodgeVec으로 고정
        if (isSwap) //무기스왑중일경우
            moveVec = Vector3.zero; //moveVec = 0(움직임 멈춤)

        transform.position += moveVec * speed * Time.deltaTime; //백터 * 스피드 * 델타타임을 위치에 계속 더해줌

        transform.LookAt(transform.position + moveVec); //플레이어가 이동방향을 바라봄

        anim.SetBool("isRun", moveVec != Vector3.zero); //에니메이션 파라미터 isRun을 움직이고 있을때 true로 설정
    }

    void Jump()
    {
        if (jDown && moveVec == Vector3.zero && !isJump && !isDodge) //점프버튼이 눌리고 점프중이 아닐경우
        {
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //위쪽벡터 * jumpForce만큼 힘을 줌
            anim.SetBool("isJump", true); //애니메이터의 sJump값을 true로 변경
            anim.SetTrigger("doJump"); //doJump 트리거 동작
            isJump = true; //isJump값 true 변경
        }
    }

    void Dodge()
    {
        if (jDown && moveVec != Vector3.zero && !isJump && !isDodge) //점프버튼이 눌리고
        {
            dodgeVec = moveVec; //회피중 벡터를 회피직전의 벡터로 고정
            speed *= 2; //속도 2배
            anim.SetTrigger("doDodge"); //doDodge 트리거 동작
            isDodge = true; //isDodge true로 변경
            Invoke("DodgeOut", 0.4f); //0.4초후 DodgeOut함수 호출
        }
    }

    void DodgeOut()
    {
        speed *= 0.5f; //속도 0.5배
        isDodge = false; //isDodge false로 변경
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Floor") //만약 접촉중인 gameobject의 태그가 "Floor"라면
        {
            anim.SetBool("isJump", false); //에니메이션 isJump 파라미터 = false
            isJump = false; //isJump = false
        }
    }
}
