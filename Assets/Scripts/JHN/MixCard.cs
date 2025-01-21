using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixCard : MonoBehaviour
{
    public SpriteRenderer frontImage;
    private string[] spritePrefixes = { "JHN_", "KDS_", "KSJ_", "SHC_", "PJW_", "BANG" };

    public void Start()
    {
        //// 스프라이트 연결해줄때 사용함
        //Transform frontTransform = transform.GetChild(0); // 첫 번째 자식
        //frontImage = frontTransform.GetComponent<SpriteRenderer>();

        //Transform frontTransform2 = transform.GetChild(1); // 첫 번째 자식
        //SpriteRenderer frontSpriteRenderer = frontTransform2.GetComponent<SpriteRenderer>();
        //frontSpriteRenderer.sprite = Resources.Load<Sprite>($"CardBack");

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

}
