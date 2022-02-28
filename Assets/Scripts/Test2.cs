using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    IEnumerator YieldTesT()
    {
        Debug.Log(1);
        yield return null;
        Debug.Log(2);
        yield return null;
        Debug.Log(3);
        yield return null;
        Debug.Log(4);
    }

    private void Start()
    {
        Debug.Log("Test2");
        YieldTesT();
        YieldTesT();
        YieldTesT();
        YieldTesT();
        YieldTesT();
    }
}
