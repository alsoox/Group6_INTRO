using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backNfront : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Front�� Back ������Ʈ ����
        CreateChildObject("Front");
        CreateChildObject("Back");
    }

    private void CreateChildObject(string childName)
    {
        // �ڽ� ������Ʈ ����
        GameObject childObject = new GameObject(childName);
        childObject.transform.parent = this.transform; // ���� ������Ʈ�� �ڽ����� ����

        // Sprite Renderer �߰�
        SpriteRenderer spriteRenderer = childObject.AddComponent<SpriteRenderer>();

        // ��ġ �� ũ�� �ʱ�ȭ
        childObject.transform.localPosition = Vector3.zero; // �θ�� ���� ��ġ
        childObject.transform.localScale = Vector3.one; // �⺻ ũ��
    }
}
