using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backNfront : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Front와 Back 오브젝트 생성
        CreateChildObject("Front");
        CreateChildObject("Back");
    }

    private void CreateChildObject(string childName)
    {
        // 자식 오브젝트 생성
        GameObject childObject = new GameObject(childName);
        childObject.transform.parent = this.transform; // 현재 오브젝트의 자식으로 설정

        // Sprite Renderer 추가
        SpriteRenderer spriteRenderer = childObject.AddComponent<SpriteRenderer>();

        // 위치 및 크기 초기화
        childObject.transform.localPosition = Vector3.zero; // 부모와 같은 위치
        childObject.transform.localScale = Vector3.one; // 기본 크기
    }
}
