using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.Intrinsics;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject card;
    public GameObject[] cardGroup;
    private GameObject nowCardGroup;


    public GameObject[] cards; // 카드들이 담긴 배열
    public Vector3[] targetPositions; // 카드들이 이동할 목표 위치

    public int round = 1;
    private int[] arr;

    void Start()
    {
        //RandomCards(round);
    }

    private IEnumerator AnimateCardsToPosition()    //카드 애니메이션
    {
        float moveDuration = 0.05f; // 이동 시간
        float timeElapsed;

        // 각 카드마다 이동
        for (int i = 0; i < cards.Length; i++)
        {
            Transform cardTransform = cards[i].transform;
            Vector3 startPosition = cardTransform.position;
            Vector3 targetPosition = targetPositions[i];

            timeElapsed = 0;
            while (timeElapsed < moveDuration)
            {
                cardTransform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / moveDuration);   // 선형보간
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            cardTransform.position = targetPosition; // (Lerp썼기 때문에)원래 위치로 설정
        }
    }
    public void RandomCards(int curRound)
    {
        if (curRound == 1)
        {
            int[] round1 = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 10, 10, 10, 10, 10 };
            round1 = round1.OrderBy(x => Random.Range(0f, 5f)).ToArray();
            arr = round1;

            int g = Random.Range(0, 3);
            nowCardGroup = cardGroup[g];

        }
        else if (curRound == 2)
        {
            int[] round2 = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 10, 10, 10, 10, 10, 10, 10 };
            round2 = round2.OrderBy(x => Random.Range(0f, 5f)).ToArray();
            arr = round2;

            int g = Random.Range(3, 6);
            nowCardGroup = cardGroup[g];
        }
        else if (curRound == 3)
        {
            int[] round3 = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
            round3 = round3.OrderBy(x => Random.Range(0f, 5f)).ToArray();
            arr = round3;

            int g = Random.Range(6, 9);
            nowCardGroup = cardGroup[g];
        }
        else{
            Debug.Log("wrong curRound");
            return;
        }

        nowCardGroup.SetActive(true);

        // 카드 및 목표 위치 초기화
        int cardCount = nowCardGroup.transform.childCount;
        cards = new GameObject[cardCount];
        targetPositions = new Vector3[cardCount];


        // CardGroup0의 자식들에 해당하는 카드 위치 배열을 가져오기
        for (int i = 0; i < cardCount; i++)
        {
            // 자식 카드 접근
            Transform child = nowCardGroup.transform.GetChild(i);
            cards[i] = child.gameObject;

            targetPositions[i] = child.position;     // 목표 위치를 현재 카드 위치로 저장

            child.position = new Vector3(0, 0, 0);  // 초기 위치 설정

            Card mixCard = child.GetComponent<Card>();

            if (curRound == 3) //라운드 3일땐 따로
            {
                if (arr[i] <= 4)
                {
                    mixCard.Setting(arr[i], 3);  // 일반 카드 (_3)
                }
                else if (arr[i] <= 9)
                {
                    int a = arr[i] - 5;
                    mixCard.Setting(a, 4);  // 일반 카드 (_4)
                }
                else
                {
                    mixCard.Setting();  // 폭탄 카드
                }

            }
            else
            {
                if (arr[i] != 10)
                    mixCard.Setting(arr[i], round);  // 일반 카드
                else
                    mixCard.Setting();  // 폭탄 카드
            }
            if (arr[i] > 4 && arr[i] <= 9) mixCard.index = arr[i] - 5;
            else mixCard.index = arr[i];
        }
        StartCoroutine(AnimateCardsToPosition());   // 카드들의 목표 위치를 설정한 후, 애니메이션 시작
    }
}
