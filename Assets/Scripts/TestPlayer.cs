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

    Rigidbody2D rigid = null;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();        
    }

    // 이 스크립트가 가진 컬라이더가 다른 컬라이더와 충돌한 직후에 실행되는 함수
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name + "와 충돌");
        OnDead();   // 죽었을 때 뭘 할지는 모르지만 죽었을때 하는 행동들이 기록된 함수를 실행

        //태그를 사용하여 바닥 충돌체크용 컬라이더가 있은 게임 오브젝트와 충돌했는지 확인                
        if ( collision.gameObject.CompareTag("Ground") )    
        {
            OnFalldown(collision.gameObject);   //바닥에 떨어졌을 때 뭘할지는 모르지만, 바닥에 떨어졌을 때 해야하는 행동들이 기록된 함수를 실행
        }
    }

    //죽었을 때 실행될 함수. 죽을 때 해야할 행동들이 기록될 함수.
    private void OnDead()
    {
        rigid.gravityScale = 1.0f;  //테스트 플레이어 전용. 죽었을 때만 중력 적용을 받음
        rigid.AddTorque(3.0f);      //리지드바디를 통해 회전력 추가
    }

    //바닥에 떨어졌을 때 실행될 함수
    private void OnFalldown(GameObject ground)
    {
        Debug.Log("땅에 부딪쳤다.");
        // GameObject.Find : 파라메터로 받은 문자열과 같은 이름을 가진 게임 오브젝트를 찾아주는 함수. 가장 비효율적

        // FindObjectOfType : 특정 타입의 컴포넌트를 가지고 있는 첫번째 게임 오브젝트를 찾아주는 함수
        // FindObjectsOfType : 특정 타입의 컴포넌트를 가지고 있는 모든 게임 오브젝트를 찾아주는 함수

        // GameObject.FindGameObjectWithTag : 파라메터로 받은 문자열과 같은 태그를 가진 첫번째 게임 오브젝트를 찾아주는 함수
        // GameObject.FindGameObjectsWithTag : 파라메터로 받은 문자열과 같은 태그를 가진 모든 게임 오브젝트를 찾아주는 함수

        // 배경을 스크롤링하는 컴포넌트(스크립트)를 찾아옴
        Scroller scroller = ground.GetComponent<Scroller>();        
        // 프로퍼티를 통해 스크롤 정지시킨다.
        scroller.ScrollSwitch = false;
    }
}
