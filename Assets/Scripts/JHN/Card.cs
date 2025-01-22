using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public SpriteRenderer frontImage;
    Transform frontTransform;
    Transform frontTransform2;
    public int index;
    private bool m_isFliping = false;
    [SerializeField] private GameObject m_backObj;

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
        Sprite[] sprites = Resources.LoadAll<Sprite>($"Picture/{spritePrefixes[idx]}{round}");
        frontImage.sprite = sprites[1];
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
        CardFold();
    }


    //animation
    public void CardOpen()
    {
        if (!m_isFliping)
        {
            StartCoroutine(CoroutineCardFlip(m_backObj, frontImage.gameObject));
        }
    }

    public void CardFold()
    {
        if (!m_isFliping)
        {
            StartCoroutine(CoroutineCardFlip(frontImage.gameObject, m_backObj));
        }
    }

    //ī�� �ִϸ��̼�
    private IEnumerator CoroutineCardFlip(GameObject _hide, GameObject _show)
    {
        // ������ ���̶�� �˸�
        m_isFliping = true;

        float filpDuration = 0.2f; // �ö� �ð� x 2(�ո�, �޸�)
        float timeElapsed;

        // ���� ������ ���� �̹��� ���� ���� ����
        Vector3 originHideScale = _hide.transform.localScale;
        Vector3 originShowScale = _show.transform.localScale;

        // X�� 0���� ����� ���� x = 0
        Vector3 zeroHideScale = _hide.transform.localScale;
        zeroHideScale.x = 0;
        Vector3 zeroShowScale = _show.transform.localScale;
        zeroShowScale.x = 0;

        _hide.SetActive(true);
        _show.SetActive(false);


        // �޸� ���� ������ origin -> 0
        timeElapsed = 0;
        while (timeElapsed < filpDuration)
        {
            _hide.transform.localScale = Vector3.Lerp(originHideScale, zeroHideScale, timeElapsed / filpDuration);   // ��������
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        // ���� �޸� �Ⱥ��̰� �ϰ� ������� ���� ����
        _hide.SetActive(false);
        _hide.transform.localScale = originHideScale;

        // ���� �ո��� ������ 0 -> origin
        _show.SetActive(true);
        timeElapsed = 0;
        while (timeElapsed < filpDuration)
        {
            _show.transform.localScale = Vector3.Lerp(zeroShowScale, originShowScale, timeElapsed / filpDuration);   // ��������
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        _show.transform.localScale = originShowScale;

        // ������ ����
        m_isFliping = false;
    }

}

