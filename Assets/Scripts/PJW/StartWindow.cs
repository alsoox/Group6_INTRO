using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWindow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // ȭ�� �ڸ���
        RefreshLetterbox(1920, 1080);
    }

        void RefreshLetterbox(float _width, float _height)
        {
            // 1920x1080 ���� (16f / 9f)
            float targetAspect = _width / _height;

            // ���� ȭ�� ���� ���
            float windowAspect = Screen.width / (float)Screen.height;

            // Ÿ�� �������� ���� ���
            float scaleHeight = windowAspect / targetAspect;

            if (scaleHeight < 1.0f)
            {
                // ȭ���� ���η� �� �� �� (���Ʒ� ���͹ڽ� �߰�)
                Rect rect = Camera.main.rect;

                rect.width = 1.0f;
                rect.height = scaleHeight;
                rect.x = 0;
                rect.y = (1.0f - scaleHeight) / 2.0f;

                Camera.main.rect = rect;
            }
            else
            {
                // ȭ���� ���η� �� �� �� (�¿� ���͹ڽ� �߰�)
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
