using System.Collections;           //네임스페이스 설정(C# 컨테이너용)
using System.Collections.Generic;   //네임스페이스 설정(C# 컨테이너용, 제네릭)
using UnityEngine;                  //네임스페이스 설정(Unity 용)

/// 접근제한자 : public(공용, 공공의), protected(보호된), private(개인적인)
/// public : 누구나 쓸 수 있다.
/// private : 가지고 있는 자만 쓸 수 있다. C# 디폴트
/// protected : 나와 나를 상속받은 자만 쓸 수 있다.
/// 키워드 : C#에서 사용을 예약해 놓은 단어들(파란색으로 보이는 부분들)
/// class : 일종의 틀. 어떤 기능을 하고 어떤 데이터를 가질 수 있는지 설정해 놓은 일종의 설계도
/// 객체 : 클래스를 인스턴스 한 것.클래스를 실체화 한 것
/// 변수 : 데이터를 저장하는 곳. 데이터 타입

//class MyTest
//{
    
//}

//Player라는 이름의 클래스를 만드는데 public이라 프로그램 내의 모두가 접근할 수 있고
//MonoBehaviour라는 클래스를 상속받았다.
public class Player : MonoBehaviour     
{
    // jumpPower라는 이름의 float 타입의 변수를 만드는데. public이고 저장되는 초기값은 10.0이다.
    public float jumpPower = 10.0f; 
    // rigid라는 이름의 Rigidbody2D 타입의 변수를 만드는데, private이고 저장되는 초기값은 null이다.
    private Rigidbody2D rigid = null;
    
    
    private void Awake()
    {
         rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Input Manager
        if( Input.GetButton("Jump") )
        {
            rigid.AddForce(Vector2.up * jumpPower);
        }
        //if(Input.GetKeyDown(KeyCode.Space))
        //{

        //}
    }
}
