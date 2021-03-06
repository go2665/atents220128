using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

//싱글톤으로 만들 GameManager
//싱글톤 : 클래스의 객체가 단 하나만 존재하게 프로그래밍하는 디자인 패턴

//static : 정적. 프로그램 실행전에 결정되어 있는 것들.

//static 변수
// 변수가 저장되는 메모리 위치가 실행전에 결정된다.
// 따라서 해당 변수를 가지는 클래스의 모든 객체가 static변수의 값은 공유한다.

//dynamic : 동적. 프로그램 실행중(Run time)에 변경되는 것들.

public class GameManager
{ 
    private static GameManager instance = null; //프로그램 전체에서 단 하나만 존재한다.
    private GameManager() { }   //생성자를 private으로 해서 밖에서 new를 할 수 없도록 한다.

    //private Text scoreText = null;      // score가 찍힐 UI text용 참조
    private ImageNumber imageNumber = null;     //score를 이미지로 보여주는 스크립트 참조
    
    // 화면에 표시되는 점수를 현재 score값으로 변경하는 함수
    private void RefreshScore()
    {
        //scoreText의 텍스트 갱신
        //scoreText.text = $"Score : {score}";

        imageNumber.Number = score;
    }

    private int highScore = 0;
    public int HighScore
    {
        get
        {
            return highScore;
        }
    }

    private int score = 0;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            RefreshScore(); //값의 변화가 있으면 자동으로 화면 갱신
        }
    }

    Player player = null;
    public Player MyPlayer
    {
        get
        {
            return player;
        }
        set
        {
            player = value;
        }
    }


    //프로퍼티 작성
    //public이라 밖에서 접근이 가능하다.
    //static 함수는 객체를 생성하지 않아도 되기 때문에 static으로 선언한다.
    public static GameManager Inst
    {
        get
        {
            if( instance == null )  // 객체 생성이 한번도 안일어났는지 확인
            {
                instance = new GameManager();   // 한번도 안일어났으면 그때 처음으로 객체 생성
                //instance.scoreText = GameObject.Find("ScoreText").GetComponent<Text>(); //ScoreText 찾아서 변수 채우기 넣을 것
                instance.imageNumber = GameObject.Find("ImageNumber").GetComponent<ImageNumber>();
                instance.LoadGameData();                
            }
            return instance;    //return까지 왔다는 것은 instance에 이미 무엇인가 할당이 되어있음
        }
    }

    public void SaveGameData()
    {
        SaveData saveData = new SaveData();
        saveData.highScore = highScore;
        //saveData.test1 = 11.22f;
        //saveData.test2 = "Test String";
        string json = JsonUtility.ToJson(saveData); //SaveData 클래스에 있는 값들을 json형식으로 바꿔라
        Debug.Log(json);
        //{ "highScore":123,"test1":11.220000267028809,"test2":"Test String"}
        
        string path = $"{Application.dataPath}/Save/";

        if( !Directory.Exists(path))   //path라는 폴더가 있는지 확인. Exists의 리턴이 true면 해당 폴더가 있다.
        {
            Directory.CreateDirectory(path);    //폴더가 없으면 만든다.
        }

        string fullPath = $"{path}Save.json";
        File.WriteAllText(fullPath, json);  //path에 json텍스트를 실제 파일로 저장 
    }

    public void LoadGameData()
    {
        string path = $"{Application.dataPath}/Save/";
        string fullPath = $"{path}Save.json";

        if ( Directory.Exists(path) && File.Exists(fullPath) )
        //if ( !Directory.Exists(path) || !File.Exists(fullPath) )
        {
            string json = File.ReadAllText(fullPath);   //path 파일에 있는 텍스트를 읽어서 json변수에 스트링으로 저장
            SaveData saveData = JsonUtility.FromJson<SaveData>(json); //json형식의 텍스트를 SaveData 클래스에 담기
            highScore = saveData.highScore;
            Debug.Log($"highScore : {saveData.highScore}");
            //Debug.Log($"Test1 : {saveData.test1}");
            //Debug.Log($"Test2 : {saveData.test2}");
        }
    }

    //게임 오버될 때 실행할 함수
    public void OnGameOver()
    {
        GameOverUI gameOverUI = GameObject.FindObjectOfType<GameOverUI>();  
        gameOverUI.OnGameOver();    //게임 오버 UI가져와서 등록된 함수 실행하기
        if ( score > highScore )    //하이스코어 일때 처리
        {
            highScore = score;      //하이스코어 갱신하고 
            SaveGameData();         //파일로 저장
        }
    }
}

//public class TestA
//{
//    public int intNum2 = 20;
//    public static int intNum = 10;
//    public static int TestTest()
//    {
//        //intNum2를 사용 못함
//        return intNum;
//    }

//    public int TestTestTest()
//    {
//        //intNum 사용 가능
//        return intNum;
//    }
//}

//public class GameManager_Test
//{ 
//    void MyTest()
//    {
//        TestA a = new TestA();
//        TestA b = new TestA();

//        TestA.intNum = 20;
//    }
//}


// 3월11일 과제
// 똥 피하기 게임 만들기
// 1. 플레이어 캐릭터 키보드 입력으로 좌우로 움직이기
// 2. 하늘에서 똥이 랜덤(x축)한 위치에서 떨어지기
// 3. 플레이어와 똥이 부딪치면 게임 끝
// 4. 게임 시작부터 끝까지 타이머 작동시키기(몇초 버텼는지 확인용)
// 완성하면 메일로 제출 : go2665@outlook.com
