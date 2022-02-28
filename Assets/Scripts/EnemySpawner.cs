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
            EnemyGenerate();

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // 적을 생생하는 것
    private void EnemyGenerate()
    {
        // 어떤 종류의 적을 생성할지 결정
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemy = GameObject.Instantiate(enemyPrefabs[enemyIndex], this.transform);
        int spaceIndex = Random.Range(0, MAX_SPACE_COUNT);
        //enemy.transform.Translate(Vector2.down * Random.Range(0.0f,2.0f));
        enemy.transform.Translate(Vector2.down * spaceIndex * SPACE_HEIGHT);
        Destroy(enemy, LIFETIME);
    }
}
