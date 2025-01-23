using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWindow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 화면 자르기
        RefreshLetterbox(1920, 1080);
    }

        void RefreshLetterbox(float _width, float _height)
        {
            // 1920x1080 비율 (16f / 9f)
            float targetAspect = _width / _height;

            // 현재 화면 비율 계산
            float windowAspect = Screen.width / (float)Screen.height;

            // 타겟 비율과의 차이 계산
            float scaleHeight = windowAspect / targetAspect;

            if (scaleHeight < 1.0f)
            {
                // 화면이 세로로 더 길 때 (위아래 레터박스 추가)
                Rect rect = Camera.main.rect;

                rect.width = 1.0f;
                rect.height = scaleHeight;
                rect.x = 0;
                rect.y = (1.0f - scaleHeight) / 2.0f;

                Camera.main.rect = rect;
            }
            else
            {
                // 화면이 가로로 더 길 때 (좌우 레터박스 추가)
                float scaleWidth = 1.0f / scaleHeight;

                Rect rect = Camera.main.rect;

                rect.width = scaleWidth;
                rect.height = 1.0f;
                rect.x = (1.0f - scaleWidth) / 2.0f;
                rect.y = 0;

                Camera.main.rect = rect;
            }
        }
}
