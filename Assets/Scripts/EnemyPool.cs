using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    //private const int DEFAULT_POOL_SIZE = 16;
    private const int DEFAULT_POOL_SIZE = 2;
    private const int RANDOM_INDEX = -1;
    private static EnemyPool instance = null;
    public static EnemyPool Inst
    {
        get
        {
            return instance;
        }
    }

    //생성 직후 한번만 호출
    private void Awake()
    {
        if (instance == null)   // 제일 처음 만들어진 인스턴스다.
        {
            instance = this;
            instance.Initialize();
            DontDestroyOnLoad(this.gameObject); // 다른 씬이 로드되더라도 삭제되지 않는다.
        }
        else
        {
            // 이미 인스턴스가 만들어진게 있다.
            if(instance != this)    // 이미 만들어진 것이 나와 다르다.
            {
                Destroy(this.gameObject);   //나는 죽는다.
            }
        }
    }

    // 생성할 적의 종류
    public GameObject[] enemyPrefabs = null;

    // 대량으로 생성해 놓은 메모리 풀
    private Queue<GameObject>[] pools = null;   //적 종류별로 만든 Queue를 저장할 배열

    // 초기화(pool변수를 채운다.)
    // 예시) (한 페이지 최대 16마리로 가정) * (3종류) = 48개. => Instantiate를 48번 한다.
    public void Initialize()
    {
        pools = new Queue<GameObject>[enemyPrefabs.Length]; // 적 종류의 수만큼 배열 크기 확정

        for( int i = 0; i<enemyPrefabs.Length; i++)
        {
            pools[i] = new Queue<GameObject>();             // 큐를 하나 생성
            for( int j = 0; j< DEFAULT_POOL_SIZE; j++)      // DEFAULT_POOL_SIZE 만큼 오브젝트 생성
            {
                GameObject obj = GameObject.Instantiate(enemyPrefabs[i], this.transform);   //생성하고 EnemyPool의 자식으로 추가
                obj.name = $"{enemyPrefabs[i].name}_{j}";   // 이름 변경
                obj.SetActive(false);   // 비활성화 상태로 변경
                pools[i].Enqueue(obj);  // 큐에 생성한 오브젝트 삽입
            }
        }
    }

    // Pool이 가지고 있는 오브젝트보다 더 많은 오브젝트가 요구되었을 때 처리하는 함수
    private void PoolExpand()
    {
        // 풀에 있는 오브젝트가 다 떨어졌을 때 확장하는 함수
    }

    // Pool에서 오브젝트를 하나 가져오는 함수(index는 생성할 종류, 기본적으로는 랜덤으로 결정)
    public GameObject GetEnemy(int index = RANDOM_INDEX)
    {
        GameObject result = null;   //리턴용으로 변수 생성
        int target = index;         //target에 실제 생성할 종류 설정

        //랜덤으로 생성되는 경우
        //index가 RANDOM_INDEX(-1)일때.
        //index가 지원 가능한 종류보다 큰 수가 들어왔을 때(ex:enemyPrefabs.Length가 3인데 index로 5를 입력받았다.)
        //index가 RANDOM_INDEX(-1) 보다 작을 때 
        if ( index == RANDOM_INDEX || index >= enemyPrefabs.Length || index < RANDOM_INDEX )
        {
            target = Random.Range(0, enemyPrefabs.Length);  // 랜덤으로 생성할 종류 결정
        }

        // 큐에 사용 가능한 오브젝트가 있는지 확인(큐가 비어있는지 확인)
        if (pools[target].Count > 0)
        {
            // 큐에 오브젝트가 있다.
            result = pools[target].Dequeue();   //Dequeue를 통해 오브젝트 하나 꺼냄
            result.SetActive(true);             // 오브젝트 활성화             
        }
        else
        {
            // 큐에 오브젝트가 없다.
            // 없으니 확장 작업 실행
            PoolExpand();   // 큐가 두배로 커지고 오브젝트도 추가되는 함수
            //result = GetEnemy(target);
        }

        return result;
    }


    //3월8일 과제
    // 1번. 난이도 최하
    // 새가 천장에 부딪쳐도 죽는다.
    //
    // 2번. 난이도 중~중상
    // 메모리 풀 초기화 함수를 완성한다.
    //
    // 3번. 난이도 중상~상하
    // Pool에서 오브젝트를 하나 가져오는 함수


    //3월10일 과제
    // Pool이 가지고 있는 오브젝트보다 더 많은 오브젝트가 요구되었을 때 처리하는 함수

}
