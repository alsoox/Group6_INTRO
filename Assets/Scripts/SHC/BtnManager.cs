using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
    public Button StartButton;
    public Button Rankingbutton;
    public Button MakerButton;


    public void GameStart()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene("RankingScene");
    }

    public void ClickStart()
    {
        SceneManager.LoadScene("MakerScene");
    }
}
