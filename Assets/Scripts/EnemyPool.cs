using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
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
    private GameObject[,] pool = null;

    // 초기화
    public void Initialize()
    {
        // pool변수를 채운다.
        // 예시) (한페이지 최대 16마리) * (3종류) = 48개. = Instantiate를 48번 한다.
    }

    // Pool이 가지고 있는 오브젝트보다 더 많은 오브젝트가 요구되었을 때 처리하는 함수
    private void PoolExpand()
    {
        // 풀에 있는 오브젝트가 다 떨어졌을 때 확장하는 함수
    }

    // Pool에서 오브젝트를 하나 가져오는 함수
    public GameObject GetEnemy()
    {
        return null;
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
    // Pool이 가지고 있는 오브젝트보다 더 많은 오브젝트가 요구되었을 때 처리하는 함수

}
