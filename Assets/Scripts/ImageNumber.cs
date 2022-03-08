using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageNumber : MonoBehaviour
{
    // 숫자를 하나 받아서 자식 오브젝트인 Num100, Num10, Num1의 스프라이트를 변경해 숫자를 표현하는 클래스

    const int DIGIT_SIZE = 3;       //3자리로 표현 할 것이라고 기획단에서 결정났기 때문에 3
    Image[] digits = new Image[DIGIT_SIZE];  
}
