using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    private Animator anim = null;
    public ImageNumber highScore = null;
    public ImageNumber myScore = null;
    public GameObject newRecordText = null;
    public GameObject restartButton = null;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        //highScore = GameObject.Find("ImageNumber_HighScore").GetComponent<ImageNumber>();
        //myScore = GameObject.Find("ImageNumber_MySoce").GetComponent<ImageNumber>();
    }

    // 게임 오버 시 실행할 함수
    public void OnGameOver()
    {
        anim.SetTrigger("GameOver");    // 애니메이션 크리거 가동
        highScore.Number = GameManager.Inst.HighScore;  // 이미지 넘버들 변경
        myScore.Number = GameManager.Inst.Score;
        if(GameManager.Inst.HighScore < GameManager.Inst.Score)
        {
            newRecordText.SetActive(true);  //하이스코어 갱신됬을 때 표시
        }
        else
        {
            newRecordText.SetActive(false);
        }
    }

    public void OnRestartButtonPress()
    {        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   // 지금 열려있는 씬을 다시 로드한다.
    }
}

//3월16일 과제
//GameOverUI 완성하기
//1. 하이스코어 표시
//2. 내 점수가 하이스코어보다 높으면 갱신(저장)
