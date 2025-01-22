using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextIntroEffect : MonoBehaviour
{
    public Text[] introTexts;  // 5개의 텍스트 UI 오브젝트 (I, N, T, R, O)
    public float animationDuration = 1f;  // 각 텍스트 애니메이션 지속 시간
    public float delayBetweenAnimations = 0.3f;  // 텍스트들 간의 간격

    void Start()
    {
        // 각 텍스트마다 코루틴을 0.3초 간격으로 시작
        for (int i = 0; i < introTexts.Length; i++)
        {
            StartCoroutine(PlayTextIntroEffect(introTexts[i], i * delayBetweenAnimations,i+1));
        }
    }

    IEnumerator PlayTextIntroEffect(Text text, float delay, int i)
    {
        // 애니메이션을 지연시키기 위해 기다리기
        yield return new WaitForSeconds(delay);

        // 텍스트의 초기 위치를 화면 밖에 두고 크기를 크게 설정
       
        text.transform.localPosition = new Vector3(-600f+(i*300), 0f, 0f);  // 화면 왼쪽 밖
        text.transform.localScale = new Vector3(5f, 5f, 1f);  // 큰 크기
        float elapsedTime = 0f;

        // 애니메이션이 끝날 때까지 반복
        while (elapsedTime < animationDuration)
        {
            // 시간에 따른 위치와 크기 보간
            float t = elapsedTime / animationDuration;
            switch (i)
            {
                case 1:
                    text.transform.localPosition = Vector3.Lerp(new Vector3(-600f, 0f, 0f), new Vector3(-360f, 190f, 0f), t);
                    break;
                case 2:
                    text.transform.localPosition = Vector3.Lerp(new Vector3(-300f, 0f, 0f), new Vector3(-190f, 190f, 0f), t); 
                    break;
                case 3:
                    text.transform.localPosition = Vector3.Lerp(new Vector3(0f, 0f, 0f), new Vector3(0f, 190f, 0f), t);
                    break;
                case 4:
                    text.transform.localPosition = Vector3.Lerp(new Vector3(300f, 0f, 0f), new Vector3(190f, 190f, 0f), t); 
                    break;
                case 5:
                    text.transform.localPosition = Vector3.Lerp(new Vector3(600f, 0f, 0f), new Vector3(360f, 190f, 0f), t); 
                    break;

            }
            text.transform.localScale = Vector3.Lerp(new Vector3(5f, 5f, 1f), new Vector3(1f, 1f, 1f), t); // 크기 줄어듦
            elapsedTime += Time.deltaTime;
            yield return null;  // 다음 프레임까지 기다림
        }

        // 최종 위치와 크기로 정확히 맞추기
        switch (i)
        {
            case 1:
                text.transform.localPosition = new Vector3(-360f, 190f, 0f); // 화면 가운데로 이동
                break;

            case 2:
                text.transform.localPosition = new Vector3(-190f, 190f, 0f); // 화면 가운데로 이동
                break;
            case 3:
                text.transform.localPosition = new Vector3(0f, 190f, 0f); // 화면 가운데로 이동
                break;
            case 4:
                text.transform.localPosition = new Vector3(190f, 190f, 0f); // 화면 가운데로 이동
                break;
            case 5:
                text.transform.localPosition = new Vector3(360f, 190f, 0f); // 화면 가운데로 이동
                break;

        }
        text.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
