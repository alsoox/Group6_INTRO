using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MakerButton : MonoBehaviour
{
    public void OnClickStart()
    {
        SceneManager.LoadScene("MakerScene");
    }
}
