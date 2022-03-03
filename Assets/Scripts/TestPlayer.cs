using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    /// 3월 2일 과제
    /// 플레이어는 다른것과 부딪치면 죽어서 바닥에 구르며 떨어진다. 그리고 화면밖으로 나간다.
    /// 조건1. 테스트플레이어는 바닥에 닿으면 죽는다.
    /// 조건2. 테스트플레이어는 적 새와 부딪치면 죽는다.    
    /// 조건3. 테스트플레이어는 죽으면 반대쪽 방향으로 구른다.(z축+방향으로 회전)
    /// 조건4. 테스트플레이어는 죽으면 조종이 안된다.(이번엔 생각안해도 됨)
    /// 조건5. 테스트플레이어가 바닥에 닿으면 잠시 뒤 배경 스크롤이 멈춘다.
    /// 
    /// 힌트
    /// OnCollisionEnter2D  //이 스크립트가 가진 컬라이더가 다른 컬라이더와 충돌 했을 때 실행되는 함수
    /// AddTorque           //Rigidbody의 맴버 함수. 회전을 더해준다.

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    void OnDead()
    {
        //죽었을 때 실행될 함수
    }
}
