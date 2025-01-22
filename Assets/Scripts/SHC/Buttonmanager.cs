using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttonmanager : MonoBehaviour
{
    public Button Button;
    public GameObject Rank_Board;
    public GamestartControll GameStartcontroll;
    public GameObject boardObject;
    public void GameStart()//게임씬 이동
    {
        //SceneManager.LoadScene("GameScene");
        GameStartcontroll.StartGame();
        Board board = boardObject.GetComponent<Board>();
        if (GameManager.Instance.round < 3)
        {
            GameManager.Instance.round++;  // round 값 증가
            board.RandomCards(GameManager.Instance.round); // 증가된 round 값을 넘겨줌
        }
    }

    public void CreditBtn()//크레딧씬 이동
    {
        SceneManager.LoadScene("CreditScene");
    }

    public void RankingBtn()//랭킹버튼
    {
        Rank_Board.SetActive(true);
    }

    public void Gameend()
    { 
        //만들어주세요
    }
}
