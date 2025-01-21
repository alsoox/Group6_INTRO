using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixCard : MonoBehaviour
{
    public SpriteRenderer frontImage;
    private string[] spritePrefixes = { "JHN_", "KDS_", "KSJ_", "SHC_", "PJW_", "BANG" };

    public void Start()
    {
        frontImage = this.GetComponent<SpriteRenderer>();
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
        frontImage.transform.localScale = new Vector3(scaleRatio, scaleRatio, 1);
    }

}
