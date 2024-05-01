using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    PlayerMovement pm;
    Animator anim;


    // Start is called before the first frame update
    void Awake()
    {
        pm = GetComponent<PlayerMovement>(); //PlayerMovement컴포넌트(클래스) 가져오기
        anim = GetComponentInChildren<Animator>(); //Amimator컴포넌트 가져오기
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isRun", pm.moveVec != Vector3.zero); //isRun을 moveVec이 0이 아니라면 true로 설정
        anim.SetBool("isWalk", pm.wDown); //isWalk를 wDown의 값으로 설정
    }
}
