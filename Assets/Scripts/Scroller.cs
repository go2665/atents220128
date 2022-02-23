using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public float scrollSpeed = 15.0f;
    private GameObject[] backgrounds = null;
    private GameObject frontBG = null;
    private GameObject rearBG = null;
    private const float END_POINT = -2.0f;
    private const float BACKGROUND_GAP = 1.43f;

    private void Awake()
    {
        backgrounds = new GameObject[transform.childCount];
        
        for(int i = 0; i< transform.childCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
        }
        frontBG = backgrounds[0];
        rearBG = backgrounds[transform.childCount - 1];
    }

    private void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            backgrounds[i].transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);
        }
        if(frontBG.transform.position.x < END_POINT)
        {
            frontBG.transform.position = new Vector3(
                rearBG.transform.position.x + BACKGROUND_GAP,
                frontBG.transform.position.y,
                0.0f);
        }
    }
}
