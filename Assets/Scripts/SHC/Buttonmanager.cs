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
    //public Button Rankingbutton;
    //public Button MakerButton;


    public void GameStart()//게임씬 이동
    {
        SceneManager.LoadScene("GameScene");
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
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit():
#endif
    }
}
