using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{   

    private void Start()
    {
        Debug.Log("Test2");
        //Random.InitState(0);
        //for (int i = 0; i < 5; i++)
        //{
        //    Debug.Log("random value : " + Random.value);
        //}

        int deleted = 3;
        deleted <<= 0;
        Debug.Log(deleted);

        //GameManager_Test t1 = new GameManager_Test();
        //GameManager t2 = new GameManager();

    }
}
