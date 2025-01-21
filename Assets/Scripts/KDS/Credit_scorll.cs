using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditScroll : MonoBehaviour
{
    public float scrollSpeed = 2f; // 배경의 스크롤 속도

    private Vector3 initialPosition;
    private float timer = 0f;

    void Start()
    {
        // 초기 위치 저장
        initialPosition = transform.position;
    }

    void Update()
    {
        // 배경을 왼쪽으로 스크롤
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
        if (transform.position.y >= 131)
        {
            scrollSpeed = 0;
            timer += Time.deltaTime;
            if (timer >= 5f)
            {
                //씬체인지
                Debug.Log("5초가 경과하여 동작을 실행합니다.");
            }
        }
    }

}


