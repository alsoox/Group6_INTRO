using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CircleFiller : MonoBehaviour
{
    public Image circleImage; // 원 이미지를 연결할 변수
    public float fillSpeed = 0.5f; // 채워지는 속도
    
    private bool isFilling = false;
    private float fillAmount = 0f;

    void Start()
    {
        // 처음에 원을 보이지 않게 숨기기
        circleImage.enabled = false;
        circleImage.fillAmount = fillAmount;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // 원 보이게 하기
            if (!circleImage.enabled)
            {
                circleImage.enabled = true;
            }
            // 원 채우기
            if (fillAmount < 1f && !isFilling)
            {
                StartCoroutine(FillCircle());
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            fillAmount = 0f;
            circleImage.fillAmount = fillAmount; // fillAmount 초기화
            isFilling = false; // Coroutine 중지
            circleImage.enabled = false;
        }
    }

    // 원을 채우는 Coroutine
    IEnumerator FillCircle()
    {
        isFilling = true;

        // 원을 채우기 시작
        while (fillAmount < 1f && Input.GetKey(KeyCode.Space))
        {
            fillAmount += Time.deltaTime * fillSpeed; // 시간을 기준으로 채워짐
            circleImage.fillAmount = fillAmount;
            yield return null; // 매 프레임 기다림
        }

        isFilling = false;

        //여기다가 씬 체인져
        SceneManager.LoadScene("MainScene");
    }


}
