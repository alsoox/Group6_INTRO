using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class RoundAnim : MonoBehaviour
{
    public Camera cameraComponent;
    public Transform cameraTransform;

    // 원래 위치
    private Vector3 initialPosition;
    private float initialOrthographicSize;
    private Quaternion initialRotation;

    void Start()
    {
        // 원래
        initialPosition = new Vector3(0f, 5f, -0.5f);
        initialOrthographicSize = 0.5f;
        initialRotation = Quaternion.Euler(90f, 90f, 90f);

        // 목표
        Vector3 startPosition = new Vector3(0f, 5f, -0.5f);
        Vector3 endPosition = new Vector3(0f, 4f, -4f);
        float endOrthographicSize = 0.9f;

        Quaternion startRotation = Quaternion.Euler(90f, 90f, 90f);
        Quaternion endRotation = Quaternion.Euler(30f, 0f, 0f);

        // 카메라 애니메이션 시작
        StartCoroutine(RotateAndMoveCamera(startPosition, endPosition, initialOrthographicSize, endOrthographicSize, startRotation, endRotation, 3f));

        // 5초 후 돌아오는 애니메이션 시작
        Invoke("RestoreCamera", 5f);
    }

    private IEnumerator RotateAndMoveCamera(Vector3 startPos, Vector3 endPos, float startSize, float endSize, Quaternion startRot, Quaternion endRot, float duration)
    {
        float elapsedTime = 0f;

        // 애니메이션 시작
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float curveValue = Mathf.SmoothStep(0f, 1f, t); // 값 보간 => 이거 쓰면 시작 끝 느리고 중간 빨라짐

            // 회전 애니메이션 (Quaternion 사용) 짐벌락 방지
            cameraTransform.rotation = Quaternion.Slerp(startRot, endRot, curveValue);

            // 위치 애니메이션
            cameraTransform.position = Vector3.Lerp(startPos, endPos, curveValue);

            // Orthographic Size 애니메이션
            cameraComponent.orthographicSize = Mathf.Lerp(startSize, endSize, curveValue);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 애니메이션 끝나면 정확한 값으로 설정
        cameraTransform.rotation = endRot;
        cameraTransform.position = endPos;
        cameraComponent.orthographicSize = endSize;
    }

    //돌아오는 애니메이션
    private void RestoreCamera()
    {
        StartCoroutine(RotateAndMoveCamera(cameraTransform.position, initialPosition, cameraComponent.orthographicSize, initialOrthographicSize, cameraTransform.rotation, initialRotation, 3f));
    }

}
