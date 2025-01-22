using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PJW
{
    public class CardBase : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer m_frontImage;
        [SerializeField] private GameObject m_backObj;

        private string[] m_spritePrefixes = { "JHN_", "KDS_", "KSJ_", "SHC_", "PJW_", "BANG" };

        private bool m_isFliping = false;

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
            Setting(0, 1);  // 일반 카드 (_3)
        }

        // 카드 사진 세팅
        public void Setting(int _idx, int _round)
        {
            m_frontImage.sprite = Resources.LoadAll<Sprite>($"Picture/{m_spritePrefixes[_idx]}{_round}")[1];
            FitSpriteToCard();
        }

        // 폭탄 카드 세팅
        public void Setting()
        {
            m_frontImage.sprite = Resources.Load<Sprite>($"Picture/BANG");
            FitSpriteToCard();
        }


        // 스프라이트 크기를 카드 크기에 맞게 조정
        private void FitSpriteToCard()
        {
            // 카드의 크기 (Transform의 localScale을 사용)
            float cardWidth = m_frontImage.transform.localScale.x;
            float cardHeight = m_frontImage.transform.localScale.y;

            // 스프라이트의 원래 크기
            Vector2 spriteSize = m_frontImage.sprite.bounds.size;

            // 비율을 맞추기 위해 스프라이트의 스케일을 계산
            float widthRatio = cardWidth / spriteSize.x;
            float heightRatio = cardHeight / spriteSize.y;

            // 스프라이트의 크기 조정
            m_frontImage.transform.localScale = new Vector3(widthRatio, heightRatio, 1);
        }

        public void CardOpen(){
            if (!m_isFliping){
                StartCoroutine(CoroutineCardFlip(m_backObj, m_frontImage.gameObject));
            }
        }
        
        public void CardFold(){
            if (!m_isFliping){
                StartCoroutine(CoroutineCardFlip(m_frontImage.gameObject, m_backObj));
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
}
