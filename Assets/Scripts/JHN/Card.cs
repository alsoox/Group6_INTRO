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
        // 시작할 때 앞 비활성화 & 버튼 설정
        frontTransform = transform.GetChild(0); // 첫 번째 자식 == Front
        frontImage = frontTransform.GetComponent<SpriteRenderer>();
        frontTransform.gameObject.SetActive(false); //비활성화
        // frontImage = this.GetComponent<SpriteRenderer>();


        frontTransform2 = transform.GetChild(1); // 두 번째 자식 == Back
       // SpriteRenderer frontSpriteRenderer = frontTransform2.GetComponent<SpriteRenderer>();
        //// 스프라이트 랜더러 삭제할 때 사용
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


    // 스프라이트 크기를 카드 크기에 맞게 조정
    private void FitSpriteToCard()
    {
        // 카드의 크기 (Transform의 localScale을 사용)
        float cardWidth = transform.localScale.x;
        float cardHeight = transform.localScale.y;

        // 스프라이트의 원래 크기
        Vector2 spriteSize = frontImage.sprite.bounds.size;

        // 비율을 맞추기 위해 스프라이트의 스케일을 계산
        float widthRatio = cardWidth / spriteSize.x;
        float heightRatio = cardHeight / spriteSize.y;

        // 두 비율 중 작은 값을 선택하여 스프라이트 크기를 카드 크기에 맞게 조정
        float scaleRatio = Mathf.Min(widthRatio, heightRatio);

        // 스프라이트의 크기 조정
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
        //cardOpenAnim.SetBool("isClose".true); //CardClose anim 추가
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

    //카드 애니메이션
    private IEnumerator CoroutineCardFlip(GameObject _hide, GameObject _show)
    {
        // 뒤집는 중이라고 알림
        m_isFliping = true;

        float filpDuration = 0.2f; // 플랍 시간 x 2(앞면, 뒷면)
        float timeElapsed;

        // 원상 복구를 위해 이미지 원래 상태 저장
        Vector3 originHideScale = _hide.transform.localScale;
        Vector3 originShowScale = _show.transform.localScale;

        // X만 0으로 만들기 위해 x = 0
        Vector3 zeroHideScale = _hide.transform.localScale;
        zeroHideScale.x = 0;
        Vector3 zeroShowScale = _show.transform.localScale;
        zeroShowScale.x = 0;

        _hide.SetActive(true);
        _show.SetActive(false);


        // 뒷면 부터 시작함 origin -> 0
        timeElapsed = 0;
        while (timeElapsed < filpDuration)
        {
            _hide.transform.localScale = Vector3.Lerp(originHideScale, zeroHideScale, timeElapsed / filpDuration);   // 선형보간
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        // 이제 뒷면 안보이게 하고 사이즈는 원상 복구
        _hide.SetActive(false);
        _hide.transform.localScale = originHideScale;

        // 이제 앞면을 보여줌 0 -> origin
        _show.SetActive(true);
        timeElapsed = 0;
        while (timeElapsed < filpDuration)
        {
            _show.transform.localScale = Vector3.Lerp(zeroShowScale, originShowScale, timeElapsed / filpDuration);   // 선형보간
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        _show.transform.localScale = originShowScale;

        // 뒤집기 끝남
        m_isFliping = false;
    }

}

