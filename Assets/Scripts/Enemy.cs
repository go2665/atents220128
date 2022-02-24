using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //이 게임 오브젝트는 게임이 시작되면 -x축 방향으로 일정한 속도로 이동한다.
    //이동하는 속도는 moveSpeed라는 변수에 저장되어 있다.
    //12:35분까지 실습 진행(쉬는시간 포함)

    //힌트
    //transform에 position에 위치값이 저장되어 있음
    //transform에 어떤 함수가 있음

    public float moveSpeed = 1.5f;

    //private void Start()
    //{
    //    //this.transform.position;    //위치 (0,0,0)
    //    //this.transform.rotation;    //회전 (0,0,0)
    //    //this.transform.localScale;  //크기배율 (1,1,1)

    //    // (0,1,1)+(1,3,1) = (1,4,2)
    //    // (1,2,3) * 2 = (2,4,6)
    //}

    private void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);        
    }

}
