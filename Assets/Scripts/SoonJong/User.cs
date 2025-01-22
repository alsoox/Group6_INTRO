using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class User : MonoBehaviour
{
    public int index; //User 값
    public GameObject Live; // 유저가 총에 맞기 전 
    public GameObject Death;// 유저가 총에 맞은 후


    void Awake()
    {

    }

    void Start()
    {
        if (gameObject.name.StartsWith("User")) // User 인덱스값 부여
        {
            string name = gameObject.name;
            if (name.Length > 4 && int.TryParse(name.Substring(4), out int result))
            {
                index = result;
            }
        }
        // ksj : 2 / pjw : 4 / jhn : 0 / kds : 1 / shc : 3
    }

    public void UserDie()
    {
        
    }
}
