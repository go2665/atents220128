using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageNumber : MonoBehaviour
{
    // 클래스의 기능
    // 숫자를 하나 받아서 자식 오브젝트인 Num100, Num10, Num1의 스프라이트를 변경해 숫자를 표현하는 클래스

    private const int DIGIT_SIZE = 3;       //3자리로 표현 할 것이라고 기획단에서 결정났기 때문에 3
    public Image[] digits = new Image[DIGIT_SIZE];  //3자리 숫자 표현할 이미지 3개(0: 100자리, 1: 10자리, 2: 1자리)
    public Sprite[] numberSprites = new Sprite[10]; //10진수 표현하는 거라 10개

    private int number = 0;  // 이미지로 변환될 숫자. 테스트를 쉽게 하려면 public으로 
    public int Number
    {
        set
        {
            number = value;
            MakeImageNumber();
        }
    }

    private void MakeImageNumber()
    {
        if( number > 999 )
        {
            number = 999;
        }

        int tempNum = number;       //예시) number = 123, tempNum = 123
        int divideNum = 100;
        for (int i=0;i<DIGIT_SIZE;i++)
        {
            //int는 소수점이 없는 숫자. 계산 결과 소수점이 나와도 int에 저장하면 소수점 이하는 사라진다.

            //각 자리수 구하기
            int digitNum = tempNum / divideNum; //자리수 구하기. 예시) digitNum = 1
            tempNum = tempNum % divideNum;      //나머지를 다음 계산용 숫자로 지정 예시) tempNum = 23
            divideNum = divideNum / 10;         //나누는 숫자의 자리수를 하나 줄임 예시) divieNum = 10

            //각 자리수에 맞게 이미지 변경
            digits[i].sprite = numberSprites[digitNum];
        }
    }

    //인스팩터 창에서 값이 성공적으로 변경했을 때 실행되는 함수
    private void OnValidate()
    {
        //Debug.Log("OnValidata");
        MakeImageNumber();

    }
}
