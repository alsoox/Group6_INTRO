using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MixCard : MonoBehaviour
{
    public SpriteRenderer frontImage;
    Transform frontTransform;
    Transform frontTransform2;
    public int index;

    private string[] spritePrefixes = { "JHN_", "KDS_", "KSJ_", "SHC_", "PJW_", "BANG" };

    public void Start()
    {
        // ������ �� �� ��Ȱ��ȭ & ��ư ����
        frontTransform = transform.GetChild(0); // ù ��° �ڽ� == Front
        frontImage = frontTransform.GetComponent<SpriteRenderer>();
        frontTransform.gameObject.SetActive(false); //��Ȱ��ȭ
        // frontImage = this.GetComponent<SpriteRenderer>();


        frontTransform2 = transform.GetChild(1); // �� ��° �ڽ� == Back
       // SpriteRenderer frontSpriteRenderer = frontTransform2.GetComponent<SpriteRenderer>();
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


    //Card
    public void DestoryCard()
    {
        Invoke("DestoryCardInvoke", 0.5f);
    }

    void DestoryCardInvoke()
    {
        Destroy(gameObject);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 0.5f);
    }

    void CloseCardInvoke()
    {
        //cardOpenAnim.SetBool("isClose".true); //CardClose anim �߰�
        frontTransform.gameObject.SetActive(false);
    }

    public void OpenCard()
    {
        //cardOpenAnim.SetBool("isOpen".true); // CardOpen anim �߰�
        frontTransform.gameObject.SetActive(true);
        frontTransform2.gameObject.SetActive(false);

        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;
        }
        else
        {
            GameManager.Instance.secondCard = this;
            GameManager.Instance.Matched();
        }

    }

}
