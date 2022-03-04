using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Instantiate : 프리팹을 게임 오브젝트로 동적 생성하는 함수
    // Destroy : 게임 오브젝트를 삭제하는 함수
    // Random.Range : 랜덤한 숫자를 돌려주는 함수

    // 위 함수들을 사용하여 게임 화면 오른쪽 끝에서 랜덤한 높이로 적 새가 생성되는 코드를 작성하시오
    // 조건1. 게임 화면 오른쪽 끝에서 새가 생성된다.
    // 조건2. 새는 랜덤한 높이로 생성된다
    // 조건3. 특정 시간 간격으로 새가 생성된다.
    // 조건4. 새는 enemyPrefabs에 지정된 프리팹 중 랜덤으로 선택된다.
    // 조건5. (조건2 취소) 새는 지정된 6칸 중 하나에서 생성된다.

    // 조건6. 새는 한번에 최소 1칸에서 최대 4칸까지 생성이 가능하다.
    // 조건7. 새는 생성될 때 최소 두개의 빈칸이 연속적으로 있어야 한다.
        // 조건6과 7을 결과는 같지만 내용이 다르게 한다면
        // 6칸을 랜덤으로 on/off 결정
        // 0번~4번을 랜덤으로 선택하고 선택된 칸과 그 다음칸은 off로 설정
        // on으로 설정된 부분만 새를 생성


    public GameObject[] enemyPrefabs = null;    // 생성할 적 새의 프리팹을 저장할 배열
    public float spawnStartDelay = 2.0f;        // 시작 딜레이 시간
    public float spawnInterval = 1.0f;          // 적들을 생성할 시간 간격

    private const int MAX_SPACE_COUNT = 6;    
    private const float SPACE_HEIGHT = 0.4f;
    private const float LIFETIME = 5.0f;

    // 최초의 Update 실행 직전에 한번만 호출
    private void Start()
    {        
        //Spawn함수를 코루틴으로 실행
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        // 실행했을 때 spawnStartDelay만큼 우선 대기
        yield return new WaitForSeconds(spawnStartDelay);
        
        // 그 후 반복해서 생성 시작
        while (true)    // 무한 루프
        {
            // bool[] flags = GetFlagsBoolType();
            int flags = GetFlags();     // flags 각 비트를 확인해서 1일 때만 새를 생성하면 된다.
                                        // 0000 0000 0000 0000 0000 0000 0010 1011 (가정)
            int singleFlag = 1;         // 시작값 0000 0000 0000 0000 0000 0000 0000 0001 

            // flags에 설정된 값에 따라 새 생성
            for (int i = 0; i < MAX_SPACE_COUNT; i++)
            {
                //if (flags[i] == true) // GetFlagsBoolType용 조건문

                // flags와 singleFlag를 &해서 0이 아니면 singleFlag에 설정된 비트 위치에 1이 되어있다는 것
                if ((flags & singleFlag) != 0)
                {                    
                    EnemyGenerate(i);
                }
                singleFlag <<= 1;   // singleFlag의 비트를 한번 검사할 때마다 왼쪽으로 한칸씩 옮김
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private int GetFlags()
    {
        // 리턴용 변수. 최종적으로 계산한 플래그 값이 들어갈 변수.
        // 6비트만 남겨 놓을 것임. 그 윗부분은 무조건 0
        // 1로 설정된 칸에서는 적 새가 생성되고
        // 0으로 설정된 칸에서는 아무것도 생성되지 않는다.
        int flags = 0;          

        while (flags == 0)  // 모든 칸이 비는 것을 방지하기 위해서 설정
        {
            int random = (int)(Random.value * 10000.0f);  //화이트보드 1번. 6비트에 각각 비트 세팅

            // (1 << MAX_SPACE_COUNT) - 1 결과로 0000 0000 0000 0000 0000 0000 0011 1111 생성
            //  1 == 0b_0000_0001
            //  MAX_SPACE_COUNT만큼 왼쪽으로 쉬프트 == 0b_0100_0000
            //  결과-1 ==  0b_0011_1111
            random &= ((1 << MAX_SPACE_COUNT) - 1); //random = random & ((1 << MAX_SPACE_COUNT) - 1);
            
            //결과 예시
            //   0101 0101 0101 0101 0101 0101 0101 0101    (random 변수의 값으로 가정)
            // & 0000 0000 0000 0000 0000 0000 0011 1111    (랜덤으로 나온값을 6bit남기는 방법, (1 << MAX_SPACE_COUNT) - 1의 결과)
            //   0000 0000 0000 0000 0000 0000 0001 0101

            //16진수 표현
            //int a = 0xff;
            //2진수 표현
            //int b = 0b_1111_1111;
            int mask = 0b_0011;
            mask = mask << Random.Range(0, MAX_SPACE_COUNT - 1);    //mask를 랜덤하게 쉬프트(아래 5개 중 하나가 되게 설정)
            //11 0000
            //01 1000
            //00 1100
            //00 0110
            //00 0011
            mask = ~mask;   //not연산을 통해 bit값 뒤집기
                            //00 1100 --> 1111 1111 1111 1111 1111 1111 1111 0011

            flags = random & mask;  //최종적으로 random값에 mask값을 & 시켜서 두칸 비우기
        }
        flags |= 0b_0001;  //테스트 용
        return flags;
    }

    private bool[] GetFlagsBoolType()
    {
        bool result = false;    // 최소 하나 이상의 새를 생성할 수 있는지 확인하는 플래그
        bool[] flags = new bool[MAX_SPACE_COUNT];   // 새가 생성될 칸이 true로 표시된 배열
        while (result == false) // 만약 하나 이상의 새를 생성할 수 없는 경우 다시 계산
        {
            // flags에 랜덤으로 값 입력. true면 해당칸의 새를 생성하고 false면 생성하지 않는다.
            for (int i = 0; i < MAX_SPACE_COUNT; i++)
            {
                if (Random.Range(0, 2) == 1)
                    flags[i] = true;
            }

            // 무조건 비워질 칸을 결정
            // flags에 설정된 값과 상관 없이 비워질 공간을 지정해 덮어 쓴다.
            int index = Random.Range(0, MAX_SPACE_COUNT - 1);
            flags[index] = false;       //랜덤으로 한칸을 무조건 비운다.
            flags[index + 1] = false;   //그 다음 칸도 비워 2개 연속된 빈 공간이 생기게 만든다.

            // 새가 하나 이상 생성되는지 확인
            for (int i = 0; i < MAX_SPACE_COUNT; i++)
            {
                // 한줄에 새가 전부 안나오는 버그 수정을 위해 flags모두 검사
                if (flags[i] == true)
                {
                    // 하나라도 true가 나왔다는 것은 빈칸이 아닌 곳이 최소 1개는 있다는 의미                        
                    result = true;      // 51번 라인의 while을 벗어나기 위해 변수 변경
                    break;              // 67번 라인의 for를 벗어나기 위해 break;
                }
            }
        }

        return flags;
    }

    // 적을 생생하는 함수
    private void EnemyGenerate(int index)
    {
        // 어떤 종류의 적을 생성할지 결정
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemy = GameObject.Instantiate(enemyPrefabs[enemyIndex], this.transform);
        //int spaceIndex = Random.Range(0, MAX_SPACE_COUNT);
        //enemy.transform.Translate(Vector2.down * Random.Range(0.0f,2.0f));
        enemy.transform.Translate(Vector2.down * index * SPACE_HEIGHT);
        Destroy(enemy, LIFETIME);
    }

    //2월 28일 과제
    // 코드가 이해되지 않은 사람 -> 주석보고 코드 이해해서 오기
    // 코드가 이해된 사람 -> bit 연산을 이용해 flags를 세팅하고 사용하도록 코드를 작성하기

    //3월 4일 과제
    // 메모리풀을 이용하여 적 새를 생성하라.
    // 못하면 조사해보기

}
