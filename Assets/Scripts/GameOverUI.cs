using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    Animator anim = null;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OnGameOver()
    {
        anim.SetTrigger("GameOver");
    }
}

//3월16일 과제
//GameOverUI 완성하기
//1. 하이스코어 표시
//2. 내 점수가 하이스코어보다 높으면 갱신(저장)
