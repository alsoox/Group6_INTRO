using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MixCard : MonoBehaviour
{
    public SpriteRenderer frontImage;
    Transform frontTransform;

    private string[] spritePrefixes = { "JHN_", "KDS_", "KSJ_", "SHC_", "PJW_", "BANG" };

    public void Start()
    {
        // ������ �� �� ��Ȱ��ȭ & ��ư ����
        frontTransform = transform.GetChild(0); // ù ��° �ڽ� == Front
        frontImage = frontTransform.GetComponent<SpriteRenderer>();
        frontTransform.gameObject.SetActive(false); //��Ȱ��ȭ
        // frontImage = this.GetComponent<SpriteRenderer>();


        Transform frontTransform2 = transform.GetChild(1); // �� ��° �ڽ� == Back
       // SpriteRenderer frontSpriteRenderer = frontTransform2.GetComponent<SpriteRenderer>();

        // ��ư ������Ʈ ����
        GameObject buttonObject = new GameObject("Button");
        buttonObject.transform.SetParent(frontTransform2); // Back �ؿ� ��ư

        // RectTransform ����
        RectTransform rectTransform = buttonObject.AddComponent<RectTransform>();
        rectTransform.localPosition = Vector3.zero; // ��ư ��ġ �ʱ�ȭ
        rectTransform.sizeDelta = new Vector2(0.8f, 1.5f); // ��ư ũ�� ����



        // Button ������Ʈ �߰�
        Button button = buttonObject.AddComponent<Button>();

        //// ��������Ʈ ������ ������ �� ���
        //SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        //Destroy(spriteRenderer);
    }
    public void Setting(int idx, int round)
    {
        frontImage.sprite = Resources.Load<Sprite>($"Picture/{spritePrefixes[idx]}{round}");
        FitSpriteToCard();
    }
    public void Setting()
    {
        frontImage.sprite = Resources.Load<Sprite>($"Picture/BANG");
        FitSpriteToCard();
    }


    // ��������Ʈ ũ�⸦ ī�� ũ�⿡ �°� ����
    private void FitSpriteToCard()
    {
        // ī���� ũ�� (Transform�� localScale�� ���)
        float cardWidth = transform.localScale.x;
        float cardHeight = transform.localScale.y;

        // ��������Ʈ�� ���� ũ��
        Vector2 spriteSize = frontImage.sprite.bounds.size;

        // ������ ���߱� ���� ��������Ʈ�� �������� ���
        float widthRatio = cardWidth / spriteSize.x;
        float heightRatio = cardHeight / spriteSize.y;

        // �� ���� �� ���� ���� �����Ͽ� ��������Ʈ ũ�⸦ ī�� ũ�⿡ �°� ����
        float scaleRatio = Mathf.Min(widthRatio, heightRatio);

        // ��������Ʈ�� ũ�� ����
        frontImage.transform.localScale = new Vector3(widthRatio, heightRatio, 1);
    }

}
