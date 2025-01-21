using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RankingButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnclickStart()
    {
        SceneManager.LoadScene("RankingScene");
    }
}
