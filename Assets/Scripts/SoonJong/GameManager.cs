using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard; // ó�� ������ ī��
    public Card secondCard; // �ι�° ������ ī�� 

    public float score;

    //public GameObject nextStageBtn;
    //public GameObject mainBtn;
    //public GameObject endingBtn;

    int count = 5; // ��Ī ���� Ƚ��  - stage 1,2 : 5 , stage 3 : 10
    int health = 5; // ����ִ� ��� ��
    int totalChance = 6; // ���þȷ귿 ��ȸ

    //public Animator successAnim; //ī�� ��Ī �� ������̼��߰�

    float time;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Time.timeScale = 1.0f;
    }
  
    void Update()
    {
        time += Time.deltaTime;
    }

    public void Matched() // ī�� ��Ī �ϱ�
    {
        if (firstCard.index == secondCard.index) // ��Ī �� ī�尡 ���� ���
        {
            firstCard.DestoryCard();  
            secondCard.DestoryCard(); // ���ī��� ��źī��� ������ �ı�

            if (firstCard.index != 10 || secondCard.index != 10)  // ��Ī �� ī�尡 ���ī���� ��� ��Ī Ƚ�� ����
            {
                count--;

                Debug.Log($"Ŭ������� ���� ��{count}");
            }

            if (count == 0) //��Ī �Ϸ� Ŭ����
            {
                Debug.Log("Ŭ����!!");
                //GameClear();
            }

            
        }

        else if (firstCard.index != secondCard.index) // ��Ī �� ī�尡 �ٸ� ���
        {
            if (firstCard.index ==  10 || secondCard.index == 10) // ��ź�� �ϳ��� ������ �̴ϰ��� ����
            {
                MiniGame();
            }

            firstCard.CloseCard();  
            secondCard.CloseCard();  //������ ī�� ���� �ʱ�ȭ
        }

        firstCard = null;
        secondCard = null;
    }

    public void MiniGame() // ���þ� �귿
    {
        Shoot(); //�Ѿ� �ݹ�

        if (health == 0)
        {
            //GameOver(); // �������?
            Debug.Log($"��� �׾����ϴ�");
        }

        Debug.Log($"�Ѿ˳�����{totalChance}");
        Debug.Log($"������{health}");

    }

    public void Shoot()
    {
        if (Random.Range(0, totalChance) == 0) // �ݹ� ����
        {
            health--;
            totalChance = 6;
            //user ���̱�
            Debug.Log("�Ѹ��׾���");
        }
        else 
        {
            totalChance--; // �ݹ� ���� �� �Ѿ˰���
        }
    }

    /*public void GameClear()
    { 
        mainBtn.SetActive(true);
        nextStageBtn.SetActive(true);
        endingBtn.SetActive(true);
    }*/

    //public void GameOver() // ��������
    //{
    //    SceneManager.LoadScene("EndingScene");
    //}
}
