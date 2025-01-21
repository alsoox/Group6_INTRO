using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;
    public Card secondCard;

    public GameObject nextStageBtn;
    public GameObject mainBtn;
    public GameObject endingBtn;

    int count = 5; // ¸ÅÄª È½¼ö - stage 1,2 : 5 , stage 3 : 10
    int health = 5;
    int totalChance = 6; // ·¯½Ã¾È·ê·¿ ±âÈ¸

    public Animator successAnim; //Ä«µå ¸ÅÄª ½Ã ¿¡µð¸ÞÀÌ¼ÇÃß°¡

    float time;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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

    public void Matched() // Ä«µå ¸ÅÄª ÇÏ±â
    {
        if (firstCard.idx == secondCard.idx) // ¸ÅÄª ÇÑ Ä«µå°¡ °°À» °æ¿ì
        {
            firstCard.DestoryCard();
            secondCard.DestoryCard();

            if (firstCard.idx != 5)
            {
                count--;
            }

            if (count == 0)
            {
                GameClear();
            }

            Debug.Log(count);
        }

        else if (firstCard.idx != secondCard.idx)
        {
            if (firstCard.idx == 5)//ÆøÅºÀÏ °æ¿ì
            {
                MiniGame();
            }

            firstCard.CloseCard();
            secondCard.CloseCard();

        }

        firstCard = null;
        secondCard = null;
    }

    public void MiniGame() // ·¯½Ã¾È ·ê·¿
    {
        Shoot();

        if (health == 0)
        {
            GameOver();
        }

        Debug.Log(totalChance);
        Debug.Log(health);

    }

    public void Shoot() // ÃÑ¾Ë ´Ù½èÀ»¶§´Â? 
    {
        if (Random.Range(0, totalChance) == 0) // °Ý¹ß ¼º°ø
        {
            health--;
            totalChance = 6;
            //user Á×ÀÌ±â
        }
        else // °Ý¹ß ½ÇÆÐ
        {
            totalChance--;
        }
    }

    public void GameClear()
    { 
        mainBtn.SetActive(true);
        nextStageBtn.SetActive(true);
        endingBtn.SetActive(true);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("EndingScene");
    }
}
