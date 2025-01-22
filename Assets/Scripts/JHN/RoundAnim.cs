using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class RoundAnim : MonoBehaviour
{
    public Camera cameraComponent;
    public Transform cameraTransform;

    // ���� ��ġ
    private Vector3 initialPosition;
    private float initialOrthographicSize;
    private Quaternion initialRotation;

    void Start()
    {
        // ����
        initialPosition = new Vector3(0f, 5f, -0.5f);
        initialOrthographicSize = 0.5f;
        initialRotation = Quaternion.Euler(90f, 90f, 90f);

        // ��ǥ
        Vector3 startPosition = new Vector3(0f, 5f, -0.5f);
        Vector3 endPosition = new Vector3(0f, 4f, -4f);
        float endOrthographicSize = 0.9f;

        Quaternion startRotation = Quaternion.Euler(90f, 90f, 90f);
        Quaternion endRotation = Quaternion.Euler(30f, 0f, 0f);

        // ī�޶� �ִϸ��̼� ����
        StartCoroutine(RotateAndMoveCamera(startPosition, endPosition, initialOrthographicSize, endOrthographicSize, startRotation, endRotation, 3f));

        // 5�� �� ���ƿ��� �ִϸ��̼� ����
        Invoke("RestoreCamera", 5f);
    }

    private IEnumerator RotateAndMoveCamera(Vector3 startPos, Vector3 endPos, float startSize, float endSize, Quaternion startRot, Quaternion endRot, float duration)
    {
        float elapsedTime = 0f;

        // �ִϸ��̼� ����
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float curveValue = Mathf.SmoothStep(0f, 1f, t); // �� ���� => �̰� ���� ���� �� ������ �߰� ������

            // ȸ�� �ִϸ��̼� (Quaternion ���) ������ ����
            cameraTransform.rotation = Quaternion.Slerp(startRot, endRot, curveValue);

            // ��ġ �ִϸ��̼�
            cameraTransform.position = Vector3.Lerp(startPos, endPos, curveValue);

            // Orthographic Size �ִϸ��̼�
            cameraComponent.orthographicSize = Mathf.Lerp(startSize, endSize, curveValue);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // �ִϸ��̼� ������ ��Ȯ�� ������ ����
        cameraTransform.rotation = endRot;
        cameraTransform.position = endPos;
        cameraComponent.orthographicSize = endSize;
    }

    //���ƿ��� �ִϸ��̼�
    private void RestoreCamera()
    {
        StartCoroutine(RotateAndMoveCamera(cameraTransform.position, initialPosition, cameraComponent.orthographicSize, initialOrthographicSize, cameraTransform.rotation, initialRotation, 3f));
    }

}
