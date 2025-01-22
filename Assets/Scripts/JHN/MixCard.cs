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
        frontImage.sprite = Resources.Load<Sprite>($"Picture/{spritePrefixes[idx]}{round}");
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
        frontTransform.gameObject.SetActive(false);
    }

    public void OpenCard()
    {
        //cardOpenAnim.SetBool("isOpen".true); // CardOpen anim 추가
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
